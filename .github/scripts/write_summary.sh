#!/bin/bash

cat <<EOF >> $GITHUB_STEP_SUMMARY
## 🧪 Test Summary

**Total:** ${{ steps.summary_data.outputs.total }}
**Passed:** ${{ steps.summary_data.outputs.passed }}
**Failed:** ${{ steps.summary_data.outputs.failed }}
**Duration:** ${{ steps.summary_data.outputs.duration }} seconds
**Failure Rate:** ${{ steps.summary_data.outputs.failure_rate }} %

### ❌ Failed Tests
${{ steps.summary_data.outputs.failed_table }}

### 🐢 Slow Tests
${{ steps.summary_data.outputs.slow_tests }}

### 🔁 Flaky Tests
${{ steps.flaky.outputs.flaky_tests }}

### 📦 Artifacts
<a href="https://github.com/${{ github.repository }}/actions/runs/${{ github.run_id }}#artifacts">
  <strong>${{ steps.zipname.outputs.zip_name }}</strong>
</a>

EOF
