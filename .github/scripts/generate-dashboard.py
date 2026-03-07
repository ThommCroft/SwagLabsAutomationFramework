import os

# Inputs from environment
total = os.environ["TOTAL"]
passed = os.environ["PASSED"]
failed = os.environ["FAILED"]
duration = os.environ["DURATION"]
failure_rate = os.environ["FAILURE_RATE"]
failed_table = os.environ["FAILED_TABLE"]
slow_tests = os.environ["SLOW_TESTS"]
new_vs_prev = os.environ["NEW_VS_PREV"]

html = f"""
<html>
<head>
<title>Test Dashboard</title>
<style>
body {{
    font-family: Arial, sans-serif;
    margin: 20px;
}}
h1 {{
    color: #333;
}}
table {{
    border-collapse: collapse;
    width: 100%;
    margin-bottom: 20px;
}}
table, th, td {{
    border: 1px solid #ccc;
}}
th, td {{
    padding: 8px;
    text-align: left;
}}
.summary-box {{
    background: #f5f5f5;
    padding: 15px;
    border-radius: 8px;
    margin-bottom: 20px;
}}
</style>
</head>
<body>

<h1>Automation Test Dashboard</h1>

<div class="summary-box">
    <p><strong>Total tests:</strong> {total}</p>
    <p><strong>Passed:</strong> {passed}</p>
    <p><strong>Failed:</strong> {failed}</p>
    <p><strong>Duration:</strong> {duration} seconds</p>
    <p><strong>Failure rate:</strong> {failure_rate}%</p>
</div>

<h2>Failed Tests</h2>
{failed_table}

<h2>Slowest Tests</h2>
{slow_tests}

<h2>Comparison with main</h2>
<pre>{new_vs_prev}</pre>

<h2>Flaky Tests</h2>
<pre>{flaky_tests}</pre>

</body>
</html>
"""

with open("test-dashboard.html", "w", encoding="utf-8") as f:
    f.write(html)
