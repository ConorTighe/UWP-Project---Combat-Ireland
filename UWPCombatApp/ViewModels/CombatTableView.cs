using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp.ViewModels
{
    class CombatTableView
    {
        public CombatDrillsTable combatDrillsTable { get; set; }

        public CombatTableView()
        {
            this.combatDrillsTable = new CombatDrillsTable();
        }
    }
}
