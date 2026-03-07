import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv("test-history.csv")

plt.figure(figsize=(10, 5))
plt.plot(df["run"], df["failure_rate"], marker="o", color="red")
plt.title("Failure Rate Over Time")
plt.xlabel("Run Number")
plt.ylabel("Failure Rate (%)")
plt.grid(True)

plt.savefig("trend-graph.png")
