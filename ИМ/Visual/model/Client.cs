using System;
using System.Collections.Generic;
using System.Text;

namespace QueuingSystem.model
{
    public class Client
    {
        private readonly int ID; // айди клиента
        private readonly Type_service type_ser; // тип сервиса
        private Time start_time; // время занесения в систему
        public Client(Time now, int ID, Type_service type_ser)
        {
            this.type_ser = type_ser;
            this.ID = ID;
            this.start_time = now;
        }
        public int get_ID() { return ID; }
        public Time Get_start_time() { return start_time; }
        public Type_service get_type() { return type_ser; }
        public void InfoTimeClient()
        {
            Console.Write("\nID: " + ID);
            Console.Write(" Время начала: "); start_time.Print();
            Console.Write(" Тип сервиса: {0}", type_ser);
        }
    }
}
