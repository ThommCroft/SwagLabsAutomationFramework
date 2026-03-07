import os
import xml.etree.ElementTree as ET

trx_file = os.environ["TRX_FILE"]
run_number = os.environ["RUN_NUMBER"]

tree = ET.parse(trx_file)
root = tree.getroot()
ns = {'t': root.tag.split('}')[0].strip('{')}

with open("test-flake-history.csv", "a", encoding="utf-8") as f:
    for unit in root.findall('.//t:UnitTestResult', ns):
        name = unit.get('testName')
        outcome = unit.get('outcome')
        f.write(f"{run_number},{name},{outcome}\n")
