﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AnvilCloud.WPF.Common.Test.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ObservableCollection<RowViewModel> _rows = new ObservableCollection<RowViewModel>()
        {
            new RowViewModel() {Name = "One", Value = "1"},
            new RowViewModel() {Name = "Two", Value = "2"},
            new RowViewModel() {Name = "Three", Value = "3"},
            new RowViewModel() {Name = "Four", Value = "4"},
            new RowViewModel() {Name = "Five", Value = "5"},
            new RowViewModel() {Name = "Six", Value = "6"},
            new RowViewModel() {Name = "Seven", Value = "7"},
            new RowViewModel() {Name = "Eight", Value = "8"},
        };

        public MainViewModel()
        {
            MoveUpCommand = new RelayCommand(MoveUp, CanMoveUp);
            SelectTwoCommand = new RelayCommand(SelectTwo);
        }

        public ICommand MoveUpCommand { get; private set; }
        public ICommand SelectTwoCommand { get; private set; }

        private void SelectTwo()
        {
            this.SelectedRows = Rows.Take(2).ToArray();
        }

        private void MoveUp()
        {
            foreach (var row in SelectedRowViewModels)
            {
                var index = Rows.IndexOf(row);

                Rows.Move(index, index - 1);    
            }

            
        }

        private bool CanMoveUp()
        {
            foreach (var row in SelectedRowViewModels)
            {
                var index = Rows.IndexOf(row);

                if (index == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public ObservableCollection<RowViewModel> Rows
        {
            get { return _rows; }
        }

        private IList _selectedItems;
        public IList SelectedRows
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged();
            }
        }

        protected RowViewModel[] SelectedRowViewModels
        {
            get
            {
                if (SelectedRows == null)
                    return new RowViewModel[]{};

                var rows = SelectedRows.OfType<RowViewModel>();

                return rows.OrderBy(r => Rows.IndexOf(r)).ToArray();
            }
        }
    }
}
