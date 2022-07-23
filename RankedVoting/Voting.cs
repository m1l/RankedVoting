using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranked_voting
{
    internal static class Voting
    {
        internal static void ProcessBallotsFromCSV(string filepath, List<int> columnsWithChoices)
        {
            List<Ballot> ballots = new List<Ballot>();
            foreach (string line in System.IO.File.ReadLines(filepath))
            {
                Ballot ballot = new Ballot(line, columnsWithChoices);
                if (ballot != null)
                {
                    ballots.Add(ballot);
                }
            }
            //remove header row with columns names
            ballots.RemoveAt(0);
            Dictionary<string, int> candidates = new Dictionary<string, int>();
            string previousRoundLoser = null;
            int round = 1;
            while (true)
            {
                ResetRoundNumbers(candidates);
                CountVotes(ballots, candidates, previousRoundLoser);
                Output($"Round {round} ---------");
                candidates = Sort(candidates);
                OutputResults(candidates);
                if (candidates.Count==2)
                    break;
                previousRoundLoser = RemoveRoundLoser(candidates);
                round++;
            }
        }

        private static void ResetRoundNumbers(Dictionary<string, int> candidates)
        {
            foreach (KeyValuePair<string, int> candidate in candidates)
                candidates[candidate.Key] = 0;
        }

        internal static void CountVotes(List<Ballot> ballots, Dictionary<string, int> candidates, string previousRoundLoser)
        {            
            int i = 0;
            while (i < ballots.Count)
            {
                var ballot = ballots[i];
                ballot.RemoveLoser(previousRoundLoser);
                string choice = ballot.GetNextChoice();
                if (choice != null)
                {
                    int votes;
                    bool exists = candidates.TryGetValue(choice, out votes);
                    if (exists)
                        candidates[choice] = votes+1;
                    else
                        candidates.Add(choice, 1);
                    i++;
                } else
                    ballots.Remove(ballot);
            }
        }

        private static Dictionary<string, int> Sort(Dictionary<string, int> candidates)
        {
            var sortedDict = from candidate in candidates orderby candidate.Value descending select candidate;
            return sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static void OutputResults(Dictionary<string, int> candidates)
        {
            var sorted = from candidate in candidates orderby candidate.Value descending select candidate;
            //return sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            int totalvotes = 0;
            foreach (var candidate in candidates)
                totalvotes += candidate.Value;

            foreach (var candidate in candidates)
            {
                double perc = (double)candidate.Value / (double)totalvotes * 100;
                Output($"{candidate.Key} has {perc.ToString("0.#")}% with {candidate.Value} votes");
            }
        }
        private static string RemoveRoundLoser(Dictionary<string, int> candidates)
        {
            var sorted = from candidate in candidates orderby candidate.Value descending select candidate;
            string loser = sorted.Last().Key;
            Output($"Removing {loser}");
            candidates.Remove(loser);
            return loser;
        }
        private static void Output(string s)
        {
            Debug.WriteLine(s);
        }
    }
}
