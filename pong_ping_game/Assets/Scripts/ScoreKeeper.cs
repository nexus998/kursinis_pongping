using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class ScoreKeeper
    {
        private int currentScore = 0;
        private int highScore = 0;

        public int GetCurrentScore() => currentScore;
        public int GetHighScore() => highScore;


        //unary op overload to increase score by 1
        public static ScoreKeeper operator ++(ScoreKeeper s)
        {
            ScoreKeeper temp = new ScoreKeeper();
            temp.currentScore = s.currentScore + 1;
            if(temp.currentScore > s.highScore)
            { temp.highScore = temp.currentScore; }
            return temp;
        }
        //binary op overload to compare 2 scores
        public static bool operator ==(ScoreKeeper s1, ScoreKeeper s2)
        {
            if (s1.currentScore == s2.currentScore) return true;
            return false;
        }
        //binary op overload to compare 2 scores
        public static bool operator !=(ScoreKeeper s1, ScoreKeeper s2)
        {
            if (s1.currentScore != s2.currentScore) return true;
            return false;
        }
        //binary op overload to compare score with int
        public static bool operator ==(ScoreKeeper s1, int s2)
        {
            if (s1.currentScore == s2) return true;
            return false;
        }
        //binary op overload to compare score with int
        public static bool operator !=(ScoreKeeper s1, int s2)
        {
            if (s1.currentScore == s2) return true;
            return false;
        }
        //---------had to do these bcus visual studio recommended.----
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return currentScore.ToString();
        }
        //----------------------------------------------------------
        
        //set currentScore to 0.
        public void ResetScore()
        {
            currentScore = 0;
        }
    }
}
