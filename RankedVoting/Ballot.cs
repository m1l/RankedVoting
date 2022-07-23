using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ranked_voting
{
    internal class Ballot
    {
        public List<string> _choices;
        public Ballot(string csvLine, List<int> columnsWithChoices)
        {
            var values = csvLine.Split(',');
            _choices = new List<string>();
            foreach (var columnIndex in columnsWithChoices)           
                _choices.Add(values[columnIndex]);
        }

        public string GetNextChoice()
        {
            while (_choices.Count > 0)
            {
                if (!string.IsNullOrEmpty(_choices[0]))
                    return _choices[0];
                _choices.RemoveAt(0);
            }
            return null;
        }
        public void RemoveLoser(string loser)
        {
            //if (!string.IsNullOrEmpty(loser))
                _choices.Remove(loser);
        }
    }
}
