using System;
using System.Collections.Generic;
using System.Text;

namespace QueuingSystem
{
    public class Client
    {
        private int ID;
        private Type_service type_ser; // тип сервиса
        private Time now_time;
        //public Time waiting_time; // ВРЕМЯ ОЖИДАНИЯ
        public Client(Time now, int num_ser, int ID, Type_service type_ser)
        {
            //Random rnd = new Random();
            this.type_ser = type_ser;//(Type_service)(rnd.Next() % 3);
            this.ID = ID;
            this.now_time = now;
            type_ser = (Type_service)(num_ser % 3);
        }
        public int get_ID() { return ID; }
        public Type_service get_type() { return type_ser; }
        public void InfoTimeClient()
        {
            Console.Write("\nID: " + ID);
            Console.Write(" Время сейчас: "); now_time.Print();
            Console.Write(" Тип сервиса: {0}", type_ser);
        }
    }
}
