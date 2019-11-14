using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lokalen.Lib
{
    public class Lokaal
    {
        private string naam;
        private sbyte verdieping;
        private char vleugel;
        private int plaatsen;
        private bool informaticalokaal;

        public string Naam
        {
            get { return naam; }
            set 
            {
                if (value == "")
                    value = "nog toe te kennen naam";
                naam = value; 
            }
        }

        public sbyte Verdieping
        {
            get { return verdieping; }
            set 
            {
                if (value < -1 || value > 2)
                    value = 0;
                verdieping = value; 
            }
        }
        public char Vleugel
        {
            get { return vleugel; }
            set 
            {
                // we wijzigen eerst de binnengekomen waarde naar hoofdletter
                string waarde = value.ToString();
                waarde = waarde.ToUpper();
                value = char.Parse(waarde);
                // controle waarden
                if (value != 'A' && value != 'B' && value != 'C' && value != 'D')
                    value = '?';
                // private variabele instellen
                vleugel = value; 
            }
        }
        public int Plaatsen
        {
            get { return plaatsen; }
            set 
            {
                if (value < 1)
                    value = 1;
                if (value > 250)
                    value = 250;
                plaatsen = value; 
            }
        }
        public bool Informaticalokaal
        {
            get { return informaticalokaal; }
            set { informaticalokaal = value; }
        }
        public Lokaal()
        {
            // properties worden NIET gevuld
            // oppassen voor null waarden!

            // Alternatief : properties hier al waarde meegeven
            //Naam = "";
            //Verdieping = 0;
            //Vleugel = ' ';
            //Plaatsen = 0;
            //Informaticalokaal = false;
        }
        public Lokaal(string naam, sbyte verdieping, char vleugel, int plaatsen, bool informaticalokaal)
        {
            // we zouden hier alle logica uit de eigenschappen opnieuw kunnen schrijven.
            // maar natuurlijk kunnen we van hieruit ook gewoon de eigenschappen vullen en de setters
            // hun werk laten doen (let op de hoofd- en kleine letters hieronder)

            Naam = naam;
            Verdieping = verdieping;
            Vleugel = vleugel;
            Plaatsen = plaatsen;
            Informaticalokaal = informaticalokaal;
        }

        public override string ToString()
        {
            return naam + " : " + vleugel + "-vleugel";
        }



    }
}
