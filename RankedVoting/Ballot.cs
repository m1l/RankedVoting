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
            _choices.RemoveAll(x => x == loser);
        }

        public bool HasAnyOf(string x)
        {
            return _choices.Contains(x);
        }

        public bool IsIncomplete
        {
            get
            {
                return _choices.Contains(string.Empty);
            }
        }

        public bool HasDuplicateChoices
        {
            get
            {
                return _choices.Distinct().Count() != _choices.Count();
            }
        }

    }
}
