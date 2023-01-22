using System;
using UnityEngine;

namespace _Project._Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Ghost[] ghosts;
        public Pacman pacman;
        public Transform pellets;

        public int ghostMultiplier { get; private set; } = 1;
        public int Score { get; private set; }
        public int Lives { get; private set; }

        private void Start()
        {
            NewGame();
        }

        private void Update()
        {
            if (Lives <= 0 && Input.anyKeyDown)
            {
                NewGame();
            }
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
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].ResetState();
            }

            pacman.ResetState();
        }

        private void GameOver()
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.SetActive(false);
            }

            pacman.gameObject.SetActive(false);
        }

        private void SetScore(int score)
        {
            Score = score;
        }

        private void SetLives(int live)
        {
            Lives = live;
        }
        
        private void ResetGhostMultiplier()
        {
            ghostMultiplier = 1;
        }

        private bool HasRemainingPellets()
        {
            foreach (Transform pellet in pellets)
            {
                if (pellet.gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        public void PelletEaten(Pellet pellet)
        {
            pellet.gameObject.SetActive(false);
            SetScore(Score + pellet.points);

            if (!HasRemainingPellets())
            {
                //Game won animation and maybe user input, new round yes or no option.
                pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);
            }
        }

        public void PowerPelletEaten(PowerPellet powerPellet)
        {
            //Change ghost state
            Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
            CancelInvoke();
            PelletEaten(powerPellet);
        }

        public void GhostEaten(Ghost ghost)
        {
            //Death animation here
            int points = ghost.points * ghostMultiplier;
            SetScore(Score + points);
            ghostMultiplier++;
        }

        public void PacmanEaten()
        {
            //Death animation here
            pacman.gameObject.SetActive(false);

            SetLives(Lives - 1);

            if (Lives > 0)
            {
                Invoke(nameof(ResetState), 3.0f);
            }
            else
            {
                GameOver();
            }

        }
    }
}