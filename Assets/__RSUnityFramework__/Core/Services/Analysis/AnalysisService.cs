using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace RSFramework.Service
{
    internal class AnalysisService : IAnalysisService
    {
        private string _currentSession;
        private readonly Dictionary<string, List<float>> _metrics = new();
        private readonly Stopwatch _stopwatch = new();

        public void StartSession(string sessionName)
        {
            _currentSession = sessionName;
            _metrics.Clear();
            _stopwatch.Restart();
            Debug.Log($"[Analysis] Session '{_currentSession}' started.");
        }

        public void EndSession()
        {
            _stopwatch.Stop();
            Debug.Log($"[Analysis] Session '{_currentSession}' ended. Duration: {_stopwatch.ElapsedMilliseconds} ms");
            PrintReport();
        }

        public void AddSample(string metricName, float value)
        {
            if (!_metrics.ContainsKey(metricName))
                _metrics[metricName] = new List<float>();

            _metrics[metricName].Add(value);
        }

        public float GetAverage(string metricName)
        {
            if (!_metrics.TryGetValue(metricName, out var samples) || samples.Count == 0)
                return 0f;

            float total = 0;
            foreach (var val in samples)
                total += val;

            return total / samples.Count;
        }

        public void PrintReport()
        {
            Debug.Log($"[Analysis] --- Report for '{_currentSession}' ---");
            foreach (var kvp in _metrics)
            {
                float avg = GetAverage(kvp.Key);
                Debug.Log($"Metric: {kvp.Key}, Samples: {kvp.Value.Count}, Average: {avg:F2}");
            }
        }

        public void Clear()
        {
            _metrics.Clear();
            Debug.Log("[Analysis] Cleared all recorded metrics.");
        }
    }internal class AnalysisService2 : IAnalysisService
    {
        private string _currentSession;
        private readonly Dictionary<string, List<float>> _metrics = new();
        private readonly Stopwatch _stopwatch = new();

        public void StartSession(string sessionName)
        {
            _currentSession = sessionName;
            _metrics.Clear();
            _stopwatch.Restart();
            Debug.Log($"[Analysis] Session '{_currentSession}' started.");
        }

        public void EndSession()
        {
            _stopwatch.Stop();
            Debug.Log($"[Analysis] Session '{_currentSession}' ended. Duration: {_stopwatch.ElapsedMilliseconds} ms");
            PrintReport();
        }

        public void AddSample(string metricName, float value)
        {
            if (!_metrics.ContainsKey(metricName))
                _metrics[metricName] = new List<float>();

            _metrics[metricName].Add(value);
        }

        public float GetAverage(string metricName)
        {
            if (!_metrics.TryGetValue(metricName, out var samples) || samples.Count == 0)
                return 0f;

            float total = 0;
            foreach (var val in samples)
                total += val;

            return total / samples.Count;
        }

        public void PrintReport()
        {
            Debug.Log($"[Analysis] --- Report for '{_currentSession}' ---");
            foreach (var kvp in _metrics)
            {
                float avg = GetAverage(kvp.Key);
                Debug.Log($"Metric: {kvp.Key}, Samples: {kvp.Value.Count}, Average: {avg:F2}");
            }
        }

        public void Clear()
        {
            _metrics.Clear();
            Debug.Log("[Analysis] Cleared all recorded metrics.");
        }
    }
}