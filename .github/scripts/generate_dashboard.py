import os
import html

total = os.environ.get("TOTAL", "0")
passed = os.environ.get("PASSED", "0")
failed = os.environ.get("FAILED", "0")
duration = os.environ.get("DURATION", "0")
failure_rate = os.environ.get("FAILURE_RATE", "0")
failed_table = os.environ.get("FAILED_TABLE", "_No failed tests._")
slow_tests = os.environ.get("SLOW_TESTS", "_No slow tests._")
new_vs_prev = os.environ.get("NEW_VS_PREV", "_No comparison available._")
flaky_tests = os.environ.get("FLAKY_TESTS", "_No flaky tests detected._")

html_content = f"""<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>Test Dashboard</title>
  <style>
    body {{
      font-family: Arial, sans-serif;
      margin: 20px;
      background-color: #f5f5f5;
    }}
    h1, h2 {{
      color: #333;
    }}
    .summary {{
      background: #fff;
      padding: 15px;
      border-radius: 8px;
      margin-bottom: 20px;
      box-shadow: 0 1px 3px rgba(0,0,0,0.1);
    }}
    pre {{
      background: #fff;
      padding: 10px;
      border-radius: 6px;
      box-shadow: 0 1px 3px rgba(0,0,0,0.1);
      overflow-x: auto;
    }}
  </style>
</head>
<body>
  <h1>Test Dashboard</h1>

  <div class="summary">
    <h2>Summary</h2>
    <p><strong>Total:</strong> {html.escape(total)}</p>
    <p><strong>Passed:</strong> {html.escape(passed)}</p>
    <p><strong>Failed:</strong> {html.escape(failed)}</p>
    <p><strong>Duration:</strong> {html.escape(duration)} seconds</p>
    <p><strong>Failure Rate:</strong> {html.escape(failure_rate)} %</p>
  </div>

  <h2>Failed Tests</h2>
  <pre>{failed_table}</pre>

  <h2>Slow Tests</h2>
  <pre>{slow_tests}</pre>

  <h2>New vs Previous Run</h2>
  <pre>{new_vs_prev}</pre>

  <h2>Flaky Tests</h2>
  <pre>{flaky_tests}</pre>
</body>
</html>
"""

output_file = "test-dashboard.html"
with open(output_file, "w", encoding="utf-8") as f:
    f.write(html_content)

print(f"Dashboard written to {output_file}")
