#!/bin/bash

cat <<EOF >> $GITHUB_STEP_SUMMARY
## 🧪 Test Summary

**Total:** $TOTAL
**Passed:** $PASSED
**Failed:** $FAILED
**Duration:** $DURATION seconds
**Failure Rate:** $FAILURE_RATE %

### ❌ Failed Tests
$FAILED_TABLE

### 🐢 Slow Tests
$SLOW_TESTS

### 🔁 Flaky Tests
$FLAKY_TESTS

### 📦 Artifacts
<a href="https://github.com/$REPOSITORY/actions/runs/$RUN_ID#artifacts">
  <strong>$ZIP_NAME</strong>
</a>

EOF
