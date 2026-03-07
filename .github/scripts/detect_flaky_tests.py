import os
import pandas as pd

history_file = "test-flake-history.csv"

if not os.path.exists(history_file):
    print("No flake history file found. Skipping flaky detection.")
    md = "_No flaky tests detected (no history yet)._"
else:
    df = pd.read_csv(history_file)

    if df.empty or "test" not in df.columns or "outcome" not in df.columns:
        md = "_No flaky tests detected (insufficient data)._"
    else:
        fail_counts = df[df["outcome"] == "Failed"].groupby("test").size()
        run_counts = df.groupby("test").size()

        flaky_tests = []
        for test in run_counts.index:
            failures = fail_counts.get(test, 0)
            total = run_counts[test]
            if 0 < failures < total:
                flaky_tests.append((test, failures, total))

        if flaky_tests:
            lines = [
                "| Test | Failures | Runs | Flakiness |",
                "|------|----------|------|-----------|",
            ]
            for test, failures, total in flaky_tests:
                rate = round((failures / total) * 100, 2)
                lines.append(f"| {test} | {failures} | {total} | {rate}% |")
            md = "\n".join(lines)
        else:
            md = "_No flaky tests detected._"

github_output = os.environ.get("GITHUB_OUTPUT")
if not github_output:
    print("GITHUB_OUTPUT is not set; cannot write step output.")
    print(md)
    exit(0)

with open(github_output, "a", encoding="utf-8") as f:
    f.write(f"flaky_tests<<EOF\n{md}\nEOF\n")
