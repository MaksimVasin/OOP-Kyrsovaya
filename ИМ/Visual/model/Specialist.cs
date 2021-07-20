using System;
using System.Collections.Generic;
using System.Text;

namespace QueuingSystem.model
{
    public class Specialist // специалист
    {
        private Status_specialist stat; // статус специалиста (свободен / занят)
        private int ID_client;
        private Time open_time; // время открытия
        private Time service_time; // время обслуживания
        public Specialist(Time arrival_time, int ID)
        {
            this.stat = Status_specialist.free;
            this.ID_client = ID;
            this.service_time = new Time(0, 0);
            this.open_time = arrival_time.add(service_time);
        }
        public void OpenStat(Time now) // установка статуса свободен
        {
            this.stat = Status_specialist.free;
            this.ID_client = 0;
            this.service_time = new Time(0, 0);
            this.open_time = now;
        }
        public void Set(Time arrival_time, int ID) // установка нового клиента к специалисту
        {
            this.stat = Status_specialist.busy; // занят
            Random rnd = new Random();
            this.ID_client = ID;
            this.service_time = new Time(0, (rnd.Next() % 20 + 5)); // время обслуживания от 5 до 25 минут
            this.open_time = arrival_time.add(this.service_time); // время открытия = время приюбытия + обсуживание
        }
        public bool Open(Time now_time) // проверка свободен ли специалист (установка статуса)
        {
            if (now_time._greater_(open_time))
            {
                this.stat = Status_specialist.free;
                return true;
            }
            else
            {
                this.stat = Status_specialist.busy;
                return false;
            }
        }
        public bool FreeSpec() // проверка статуса
        {
            if (this.stat == Status_specialist.free) return true;
            else return false;
        }
        public int ID_Client() { return ID_client; }
        public String Open_time() { return open_time.Get_time_string(); }
        public String Service_time() { return service_time.Get_time_string(); }
        public void InfoService()
        {
            if (this.stat == Status_specialist.free)
            {
                Console.Write(" free");
            }
            else
            {
                Console.Write(" ID = " + ID_client);
                Console.Write(" Открытие: "); open_time.Print();
                Console.Write(" Обслуживание: = "); service_time.Print();
            }
        }
    }
}
