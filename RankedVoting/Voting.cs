using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Ranked_voting
{
    internal static class Voting
    {
        static List<string> _output;
        internal static string ProcessBallotsFromCSV(string filepath, List<int> columnsWithChoices)
        {
            _output = new List<string>();

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

            int totalBallots = ballots.Count;
            Output($"Total {totalBallots} ballots.");
            OutputIncompleteCount(ballots);
            OutputBallotsWithWastedVotes(ballots);
            Output("------------------");

            Dictionary<string, int> candidates = new Dictionary<string, int>();
            string previousRoundLoser = string.Empty;
            int round = 1;
            while (true)
            {
                Output($"Round {round}");

                ResetRoundNumbers(candidates);
                if (!string.IsNullOrEmpty(previousRoundLoser))
                    RemoveLoserFromAllBallots(ballots, previousRoundLoser);

                CountVotes(ballots, candidates);

                candidates = Sort(candidates);
                OutputRoundResults(candidates);
                
                if (candidates.Count==2)
                    break;
                previousRoundLoser = RemoveRoundLoser(candidates, ballots);
                round++;

                Output("------------------");
            }
            OutputSummary(candidates, totalBallots);
            return string.Join(Environment.NewLine, _output);
        }

       

      

        private static void ResetRoundNumbers(Dictionary<string, int> candidates)
        {
            foreach (KeyValuePair<string, int> candidate in candidates)
                candidates[candidate.Key] = 0;
        }
        private static void RemoveLoserFromAllBallots(List<Ballot> ballots, string loser)
        {
            foreach (Ballot ballot in ballots)
                ballot.RemoveLoser(loser);
        }
        internal static void CountVotes(List<Ballot> ballots, Dictionary<string, int> candidates)
        {            
            int i = 0;
            while (i < ballots.Count)
            {
                var ballot = ballots[i];
               
                string choice = ballot.GetNextChoice();
                
                if (choice != null)
                {                   
                    int votes;
                    bool exists = candidates.TryGetValue(choice, out votes);
                    if (exists)
                        candidates[choice] = votes + 1;
                    else
                        candidates.Add(choice, 1);
                    i++;
                } 
                else
                    ballots.Remove(ballot);
            }
        }

        private static Dictionary<string, int> Sort(Dictionary<string, int> candidates)
        {
            var sortedDict = from candidate in candidates orderby candidate.Value descending select candidate;
            return sortedDict.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static void OutputRoundResults(Dictionary<string, int> candidates)
        {
            var sorted = from candidate in candidates orderby candidate.Value descending select candidate;
            int totalvotes = 0;
            foreach (var candidate in candidates)
                totalvotes += candidate.Value;

            foreach (var candidate in candidates)
            {                
                var perc = decimal.Divide(candidate.Value, totalvotes) * 100;
                Output($"{candidate.Key} has {perc.ToString("0.#")}% with {candidate.Value} votes");
            }
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
            {
                Output(string.Empty);
                Output($"Tie for removal! Candidates {string.Join(", ", ties.ToArray())} have {loservotes} votes");
                loser = BreakTieReturnLoser(ties, ballots);
            }
            //&&
            //sorted.ElementAtOrDefault(n-1).Value == sorted.ElementAtOrDefault(n-2).Value)
            //loser = BreakTieReturnLoser(ballots, sorted.ElementAtOrDefault(n-1).Key, sorted.ElementAtOrDefault(n-2).Key);
            Output(string.Empty);
            Output($"{loser} lost this round");
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
                Output($"Candidate {candidate} has {othervotes} votes total");
                if (othervotes < lowestCount)
                {
                    loser = candidate;
                    lowestCount = othervotes;
                }
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

        private static void OutputSummary(Dictionary<string, int> candidates, int totalBallots)
        {
            var winner = candidates.First();
            Output(string.Empty);
            Output($"Candidate {winner.Key} won with {winner.Value} votes out of {totalBallots} ballots");
            var percOfTotal = decimal.Divide(winner.Value, totalBallots) * 100;
            Output($"{percOfTotal.ToString("0.##")}% selected {winner.Key} as one of their preferences");
        }
        private static void OutputIncompleteCount(List<Ballot> ballots)
        {
            int totalIncomplete = 0;
            foreach (Ballot ballot in ballots)
                if (ballot.IsIncomplete)
                    totalIncomplete++;
            Output($"Total {totalIncomplete} incomplete ballots");
        }
        private static void OutputBallotsWithWastedVotes(List<Ballot> ballots)
        {
            int totalWasted = 0;
            foreach (Ballot ballot in ballots)
                if (ballot.HasDuplicateChoices)
                    totalWasted++;
            Output($"Total {totalWasted} ballots with wasted votes");
        }
        private static void Output(string s)
        {
            _output.Add(s);
        }
    }
}
