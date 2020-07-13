using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;

namespace ToyProject
{
    class MainWindowViewModel : BasePropertyChanged
    {
        private int[,] glist;
        private int[,] slist;
        private int[,,] blist;
        private int randseed;
        private List<KeyValuePair<int, int>> zlist;

        public MainWindowViewModel()
        {
            zlist = new List<KeyValuePair<int, int>>();
            glist = new int[10, 10];
            slist = new int[10, 10];
            blist = new int[3, 3, 10];
        }

        private ObservableCollection<ObservableCollection<int>> _sudoku;
        public ObservableCollection<ObservableCollection<int>> Sudoku
        {
            get
            {
                if (_sudoku == null)
                {
                    _sudoku = new ObservableCollection<ObservableCollection<int>>();
                    for (int i = 0; i <= 9; i++)
                    {
                        _sudoku.Add(new ObservableCollection<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                    }
                }
                return _sudoku;
            }
            set
            {
                _sudoku = value;
                OnPropertyChanged(nameof(Sudoku));
            }
        }

        public void Create(int seed = 1)
        {
            zlist = new List<KeyValuePair<int, int>>();
            glist = new int[10, 10];
            slist = new int[10, 10];
            blist = new int[3, 3, 10];
            randseed = seed;
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    int c = _sudoku[i][j];
                    if (c == 0)
                    {
                        zlist.Add(new KeyValuePair<int, int>(i, j));
                    }
                    else
                    {
                        glist[i, c] = 1;
                        slist[c, j] = 1;
                        blist[(i - 1) / 3, (j - 1) / 3, c] = 1;
                    }
                }
            }
            bool IsSolved = false;
            int cnt = 0;
            r(ref _sudoku, 0, ref cnt, ref IsSolved);
            if (!IsSolved) throw new Exception("Unable to Solve");          
            return;
        }

        private void r(ref ObservableCollection<ObservableCollection<int>> arr, int zindex, ref int cnt, ref bool IsSolved)
        {
            if (IsSolved && cnt >= randseed) return;
            if (zindex == zlist.Count)
            {
                IsSolved = true;
                cnt++;
                if (cnt >= randseed && IsSolved)
                {
                    Sudoku = arr;                    
                }
                return;
            }            
            int x = zlist[zindex].Key;
            int y = zlist[zindex].Value;

            for (int i = 1; i <= 9; i++)
            {
                if (glist[x, i] == 0 && slist[i, y] == 0 && blist[(x - 1) / 3, (y - 1) / 3, i] == 0)
                {
                    glist[x, i] = 1;
                    slist[i, y] = 1;
                    blist[(x - 1) / 3, (y - 1) / 3, i] = 1;
                    arr[x][y] = i;
                    r(ref arr, zindex + 1, ref cnt, ref IsSolved);                    
                    glist[x, i] = 0;
                    slist[i, y] = 0;
                    blist[(x - 1) / 3, (y - 1) / 3, i] = 0;
                    arr[x][y] = 0;
                }
            }
        }
    }

    public class BasePropertyChanged : INotifyPropertyChanged
    {
        #region 속성 

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }

        #endregion
    }
}
