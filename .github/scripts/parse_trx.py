import os
import xml.etree.ElementTree as ET

trx_file = os.environ["TRX_FILE"]
prev_trx = os.environ.get("PREV_TRX_PATH") or ""
has_prev = (os.environ.get("HAS_PREV_FLAG") or "").lower() == "true"
artifact_url = os.environ.get("TEST_RESULTS_ARTIFACT_URL") or ""

def parse_trx(path):
    tree = ET.parse(path)
    root = tree.getroot()
    ns = {'t': root.tag.split('}')[0].strip('{')}

    results = []
    total = passed = failed = 0
    duration_total = 0.0

    for unit in root.findall('.//t:UnitTestResult', ns):
        name = unit.get('testName')
        outcome = unit.get('outcome')
        dur = unit.get('duration') or "0:00:00"
        parts = dur.split(':')

        try:
            seconds = float(parts[-1]) + int(parts[-2]) * 60 + int(parts[-3]) * 3600
        except Exception:
            seconds = 0.0

        total += 1
        if outcome == "Passed":
            passed += 1
        elif outcome == "Failed":
            failed += 1

        duration_total += seconds
        results.append({
            "name": name,
            "outcome": outcome,
            "seconds": seconds
        })

    return {
        "total": total,
        "passed": passed,
        "failed": failed,
        "duration": duration_total,
        "results": results
    }

cur = parse_trx(trx_file)

prev = None
if has_prev and prev_trx and os.path.isfile(prev_trx):
    prev = parse_trx(prev_trx)

total = cur["total"]
passed = cur["passed"]
failed = cur["failed"]
duration = cur["duration"]
failure_rate = (failed / total * 100.0) if total else 0.0

failed_tests = [r for r in cur["results"] if r["outcome"] == "Failed"]

def sanitize(name):
    return ''.join(c if c.isalnum() or c in ('_', '-', '.') else '_' for c in name.replace(' ', '_'))

failed_table_lines = []
if failed_tests:
    failed_table_lines.append("| Test | Screenshot | Trace | Viewer |")
    failed_table_lines.append("|------|------------|--------|---------|")
    for r in failed_tests:
        name = r["name"]
        file_base = sanitize(name)
        screenshot = f"Screenshots/{file_base}.png"
        trace = f"Traces/{file_base}.zip"
        viewer = "[🔍](https://trace.playwright.dev/)"
        failed_table_lines.append(f"| {name} | `{screenshot}` | `{trace}` | {viewer} |")
else:
    failed_table_lines.append("_No failed tests._")

failed_table_md = "\n".join(failed_table_lines)

sorted_by_time = sorted(cur["results"], key=lambda r: r["seconds"], reverse=True)
slowest = sorted_by_time[:5]

slow_lines = []
if slowest:
    slow_lines.append("| Test | Duration (s) | Outcome |")
    slow_lines.append("|------|--------------|---------|")
    for r in slowest:
        slow_lines.append(f"| {r['name']} | {r['seconds']:.2f} | {r['outcome']} |")
else:
    slow_lines.append("_No tests found._")

slow_md = "\n".join(slow_lines)

new_fail_md = "_No previous main run to compare._"
if prev:
    prev_failed = {r["name"] for r in prev["results"] if r["outcome"] == "Failed"}
    cur_failed = {r["name"] for r in cur["results"] if r["outcome"] == "Failed"}

    new_failures = sorted(cur_failed - prev_failed)
    still_failing = sorted(cur_failed & prev_failed)
    fixed = sorted(prev_failed - cur_failed)

    lines = []
    if new_failures:
        lines.append("**🆕 New failures:**")
        for n in new_failures:
            lines.append(f"- {n}")
        lines.append("")
    if still_failing:
        lines.append("**🔁 Still failing:**")
        for n in still_failing:
            lines.append(f"- {n}")
        lines.append("")
    if fixed:
        lines.append("**🧹 Fixed since main:**")
        for n in fixed:
            lines.append(f"- {n}")
        lines.append("")
    if not lines:
        lines.append("_No differences in failures compared to main._")

    new_fail_md = "\n".join(lines)

def gh_set_output(name, value):
    with open(os.environ["GITHUB_OUTPUT"], "a", encoding="utf-8") as f:
        f.write(f"{name}<<EOF\n{value}\nEOF\n")

gh_set_output("total", str(total))
gh_set_output("passed", str(passed))
gh_set_output("failed", str(failed))
gh_set_output("duration", f"{duration:.2f}")
gh_set_output("failure_rate", f"{failure_rate:.2f}")
gh_set_output("failed_table", failed_table_md)
gh_set_output("slow_tests", slow_md)
gh_set_output("new_vs_prev", new_fail_md)
gh_set_output("artifact_url", artifact_url)
