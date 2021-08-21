using System;

namespace WPF_restauration.UIModels
{
    public class UITable
    {
        private readonly MainWindow _mainWindow;
        private readonly Action<MainWindow, string> _updateAction;
        public UITable(MainWindow mainWindow, Action<MainWindow, string> updateAction)
        {
            _mainWindow = mainWindow;
            _updateAction = updateAction;
        }

        public void UpdateTable(string name)
        {
            _updateAction(_mainWindow, name);
        }
    }
}
