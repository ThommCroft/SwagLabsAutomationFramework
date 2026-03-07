# SwagLabsAutomationFramework
A repository for End-to-End testing using the Swag Labs website, ReqnRoll and Playwright.

🚀 Automated Testing & Continuous Integration
This repository includes a fully automated, production‑grade CI system built on GitHub Actions. It runs Playwright tests, generates dashboards, tracks historical trends, detects flaky tests, and provides rich feedback on pull requests and nightly runs.

The goal is simple: fast feedback, deep visibility, and long‑term test stability.

🧪 Pull Request Test Pipeline
Every pull request triggers a full test run that includes:

✔ Playwright test execution
Runs the .NET Playwright test suite with screenshots, traces, and TRX output.

✔ TRX parsing
A custom parser extracts:

Total tests

Passed

Failed

Duration

Failure rate

Failed test table

Slow tests

New vs previous run (if applicable)

✔ Artifact uploads
Each PR run uploads:

TRX results

Screenshots

Traces

HTML dashboard

✔ Flaky test detection
A test is marked flaky if it fails at least once and passes at least once across historical runs.

✔ PR comment
A structured comment is posted automatically with:

Summary

Failed tests

Slow tests

Flaky tests

Dashboard link

✔ Email notifications
If the PR run has failures, an email is sent to the configured recipients.

🌙 Nightly Regression Pipeline
Runs automatically every night at midnight UK time.

What it does:
Executes the full test suite

Parses TRX results

Updates long‑term test history

Updates per‑test flake history

Generates a trend graph (trend-graph.png)

Generates a full HTML dashboard (test-dashboard.html)

Uploads all artifacts

Sends an email if failures occur

Long‑term tracking includes:
Failure rate over time

Per‑test flakiness

Slow test patterns

Run‑to‑run comparisons

This gives you a clear picture of test stability and quality trends.

🔁 Flaky Test Detection
Flaky tests are automatically identified using historical data.

A test is considered flaky if:

It fails at least once

It passes at least once

Flaky tests appear in:

PR comments

Nightly dashboards

Email notifications

📈 Historical Trend Tracking
Two CSV files maintain long‑term data:

test-history.csv
Tracks run‑level metrics:

Run number

Total tests

Passed

Failed

Failure rate

test-flake-history.csv
Tracks per‑test outcomes:

Run number

Test name

Outcome (Passed/Failed)

A nightly trend graph visualises failure rate over time.

🖥️ HTML Dashboard
Each run generates a dashboard containing:

Summary

Failed tests

Slow tests

New vs previous run

Flaky tests

This dashboard is uploaded as an artifact and can be viewed locally.

📧 Email Notifications
Emails are sent when:

PR tests fail

Nightly tests fail

Required secrets:
Secret	Description
SMTP_USERNAME	Gmail address used to send mail
SMTP_PASSWORD	Gmail App Password
EMAIL_RECIPIENTS	Comma‑separated list of recipients

🛠️ Setup Instructions
1. Add GitHub Secrets
Go to:
Settings → Secrets and variables → Actions

## 📧 SMTP Configuration

| Secret Name        | Description                                   | Required | Example Value                          |
|--------------------|-----------------------------------------------|----------|------------------------------------------|
| SMTP_USERNAME      | Email address used to send notifications       | Yes      | yourname@gmail.com                       |
| SMTP_PASSWORD      | Gmail App Password (not your real password)   | Yes      | abcd efgh ijkl mnop                      |
| EMAIL_RECIPIENTS   | Comma‑separated list of people to notify      | Yes      | dev1@example.com, dev2@example.com       |

2. Gmail SMTP Setup
Enable 2‑Step Verification

Create an App Password

Store it in SMTP_PASSWORD

3. File Structure
4. .github/
  workflows/
    pr-tests.yml
    nightly-regression.yml
  scripts/
    parse_trx.py
    update_flake_history.py
    detect_flaky_tests.py
    generate_dashboard.py
    generate_trend_graph.py
AutomationFramework/
  ... your test project ...

📂 Workflows Included
1. PR Test Pipeline
Located at:
.github/workflows/pr-tests.yml

Triggers on:

Pull requests
Pushes to main

2. Nightly Regression Pipeline
Located at:
.github/workflows/nightly-regression.yml

Triggers at:

Midnight UK time every day  
(0 23 * * * in UTC)

