import os
import xml.etree.ElementTree as ET

trx_file = os.environ.get("TRX_FILE")
run_number = os.environ.get("RUN_NUMBER", "")

if not trx_file:
    print("TRX_FILE environment variable is not set.")
    exit(1)

if not os.path.exists(trx_file):
    print(f"TRX file not found: {trx_file}")
    exit(1)

try:
    tree = ET.parse(trx_file)
except Exception as e:
    print(f"Failed to parse TRX file '{trx_file}': {e}")
    exit(1)

root = tree.getroot()
ns = {"t": root.tag.split("}")[0].strip("{")}

with open("test-flake-history.csv", "a", encoding="utf-8") as f:
    for unit in root.findall(".//t:UnitTestResult", ns):
        name = unit.get("testName")
        outcome = unit.get("outcome")
        if name and outcome:
            f.write(f"{run_number},{name},{outcome}\n")
