import os
import pandas as pd

df = pd.read_csv("test-flake-history.csv")

# Count failures per test
fail_counts = df[df["outcome"] == "Failed"].groupby("test").size()

# Count total runs per test
run_counts = df.groupby("test").size()

flaky_tests = []

for test in run_counts.index:
    failures = fail_counts.get(test, 0)
    total = run_counts[test]

    # Flaky if failed at least once but not always
    if 0 < failures < total:
        flaky_tests.append((test, failures, total))

# Build markdown table
if flaky_tests:
    lines = [
        "| Test | Failures | Runs | Flakiness |",
        "|------|----------|------|-----------|"
    ]
    for test, failures, total in flaky_tests:
        rate = round((failures / total) * 100, 2)
        lines.append(f"| {test} | {failures} | {total} | {rate}% |")
    md = "\n".join(lines)
else:
    md = "_No flaky tests detected._"

# Output to GitHub Actions
with open(os.environ["GITHUB_OUTPUT"], "a") as f:
    f.write(f"flaky_tests<<EOF\n{md}\nEOF\n")
