using System;
using UnityEngine;

namespace _Project._Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Ghost[] ghosts;
        public Pacman pacman;
        public Transform pellets;
        
        public int Score { get; private set; }
        public int Lives { get; private set; }

        private void Start()
        {
            NewGame();
        }

        private void NewGame()
        {
            SetScore(0);
            SetLives(3);
            NewRound();
        }

        private void NewRound()
        {
            foreach (Transform pellet in pellets)
            {
                pellet.gameObject.SetActive(true);
            }
            
            ResetState();
        }

        private void ResetState()
        {
            SetActivePacmanAndGhosts(true);
        }

        private void GameOver()
        {
            SetActivePacmanAndGhosts(false);
        }

        private void SetScore(int score)
        {
            Score = score;
        }

        private void SetLives(int live)
        {
            Lives = live;
        }

        private void SetActivePacmanAndGhosts(bool state)
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(state);
            }
            
            pacman.gameObject.SetActive(state);
        }
        
        public void GhostEaten(Ghost ghost)
        {
            //Death animation here
            SetScore(Score + ghost.points);
        }

        public void PacmanEaten()
        {
            //Death animation here
            pacman.gameObject.SetActive(false);
            
            SetLives(Lives - 1);

            if (Lives < 0)
            {
                Invoke(nameof(ResetState), 3f);
            }
            
            GameOver();
        }
    }
}
