using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MenuItem> menuElements;
    }

    public enum MenuType
    {
        Lunch = 1,
        Dinner = 2,
        Drinks = 3
    }

    public enum MenuCategory
    {
        Starter,
        Main,
        Entremet,
        Dessert,
        SoftDrink,
        Beer,
        Wine,
        SpiritDrink,
        Coffee
    }
}
