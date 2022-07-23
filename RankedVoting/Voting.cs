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
        internal static string ProcessBallotsFromCSV(string filepath, List<int> columnsWithChoices)
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
            string results = string.Empty;
            while (true)
            {
                ResetRoundNumbers(candidates);
                CountVotes(ballots, candidates, previousRoundLoser);
                results += $"Round {round}" + Environment.NewLine + Environment.NewLine;
                candidates = Sort(candidates);
                results += OutputRoundResults(candidates);
                results += Environment.NewLine;
                if (candidates.Count==2)
                    break;
                previousRoundLoser = RemoveRoundLoser(candidates, ballots);
                results += $"Removing {previousRoundLoser}" + Environment.NewLine + Environment.NewLine;
                round++;
            }
            return results;
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

        private static string OutputRoundResults(Dictionary<string, int> candidates)
        {
            var sorted = from candidate in candidates orderby candidate.Value descending select candidate;
            //return sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            int totalvotes = 0;
            foreach (var candidate in candidates)
                totalvotes += candidate.Value;

            string result = string.Empty;
            foreach (var candidate in candidates)
            {
                double perc = (double)candidate.Value / (double)totalvotes * 100;
                //Output($"{candidate.Key} has {perc.ToString("0.#")}% with {candidate.Value} votes");
                result += $"{candidate.Key} has {perc.ToString("0.#")}% with {candidate.Value} votes" + Environment.NewLine;
            }
            return result;
        }
        private static string RemoveRoundLoser(Dictionary<string, int> candidates, List<Ballot> ballots)
        {
            var sorted = from candidate in candidates orderby candidate.Value descending select candidate;
            string loser = sorted.Last().Key;

            //break tie
            int loservotes = sorted.Last().Value;
            var tie = candidates.Where(x => x.Value == loservotes);
            var ties = tie.Select(x => x.Key);
            int n = sorted.Count();
            if (ties.Count() > 1)
                loser = BreakTieReturnLoser(ties, ballots);
                //&&
                //sorted.ElementAtOrDefault(n-1).Value == sorted.ElementAtOrDefault(n-2).Value)
                //loser = BreakTieReturnLoser(ballots, sorted.ElementAtOrDefault(n-1).Key, sorted.ElementAtOrDefault(n-2).Key);

            candidates.Remove(loser);
            return loser;
        }
        private static string BreakTieReturnLoser(IEnumerable<string> candidates, List<Ballot> ballots)
        {
            List<int> recount = new List<int>();
            int lowestCount = int.MaxValue;
            string loser = String.Empty;
            foreach (var candidate in candidates)
            {
                int othervotes = 0;
                foreach (Ballot ballot in ballots)
                    if (ballot.HasAnyOf(candidate))
                        othervotes++;
                if (othervotes < lowestCount)
                    loser = candidate;          
                else if (othervotes == lowestCount)
                {
                    Random rand = new Random();
                    loser = rand.Next(0, 1) == 0 ? loser : candidate;
                }
            }
            return loser;
        }

        private static string BreakTieReturnLoser(List<Ballot> ballots, string can1, string can2)
        {
            int count1 = 0;
            int count2 = 0;
            foreach (var ballot in ballots)
            {
                if (ballot.HasAnyOf(can1))
                     count1++;
                if (ballot.HasAnyOf(can2))
                     count2++;
            }
            if (count1 == count2)
            {
                Random rand = new Random();
                return rand.Next(0,1) == 0 ? can1 : can2;
            }
            return count1 < count2 ? can1 : can2;
        }

    }
}
