import os
import pandas as pd
import matplotlib.pyplot as plt

history_file = "test-history.csv"

if not os.path.exists(history_file):
    print("No test-history.csv found. Skipping trend graph.")
    exit(0)

df = pd.read_csv(history_file)

if df.empty or "run" not in df.columns or "failure_rate" not in df.columns:
    print("Insufficient data in test-history.csv to generate trend graph.")
    exit(0)

df = df.sort_values("run")

plt.figure(figsize=(8, 4))
plt.plot(df["run"], df["failure_rate"], marker="o")
plt.title("Test Failure Rate Over Time")
plt.xlabel("Run")
plt.ylabel("Failure Rate (%)")
plt.grid(True)
plt.tight_layout()

output_file = "trend-graph.png"
plt.savefig(output_file)
print(f"Trend graph saved to {output_file}")
