using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BeeSouls
{
    public class ScoreManager
    {
        private static string _scoreFilename = "score.xml";
        public List<Score> Scores { get; set; }

        private ScoreManager()
        {
            Scores = new List<Score>();
        }

        public void AddScore(string name, int val)
        {
            Scores.Add(new Score() { PlayerName = name, Value = val});
        }

        public void SaveScores()
        {
            ObjectXMLSerializer<ScoreManager>.Save(this, _scoreFilename);
        }

        public static ScoreManager LoadScore()
        {
            try
            {
                ScoreManager manager = ObjectXMLSerializer<ScoreManager>.Load(_scoreFilename);
                return manager;
            }
            catch 
            {
                return new ScoreManager();
               
            }
        }
    }
}
