using System;
using System.Collections.Generic;
using System.Text;

namespace QueuingSystem.model
{
    public class Service
    {
        private Type_service type_ser; // тип сервиса (услуги)
        private int num; // количество специалистов
        public Specialist[] spec; // массив специалистов (один вид услуги)
        private List<Client> QueueService; // очередь к услуге
        private List<Time> WaitingTime; // лист времени ожидания
        public List<Client> Get_queue() { return QueueService; }
        public Service(int num_of_spec, Time now, Type_service ser)
        {
            this.num = num_of_spec;
            this.spec = new Specialist[this.num];
            this.QueueService = new List<Client>();
            this.WaitingTime = new List<Time>();
            this.type_ser = ser;
            for (int i = 0; i < this.num; i++)
                spec[i] = new Specialist(now, 0);
        }
        public void AddQueue(Client cur) // добавить в очередь
        {
            QueueService.Add(cur);
        }
        public void Clear() // очитска очереди
        {
            QueueService.Clear();
        }
        // метод выбирает рандомное число и определяет в каком оно промежутке
        // входные парметры - процентные соотношения услуг (популярность)
        static public int Rand_t_s(int percent_p, int percent_v, int percent_r, int rnd) 
        {
            try
            {
                int type;
                if (percent_p + percent_r + percent_v != 100) throw new ArgumentException("Incorrect input");
                if (rnd % 100 <= percent_p) type = 0;
                else if (rnd % 100 > percent_p && rnd % 100 < percent_p + percent_v) type = 1;
                else type = 2;
                return type;
            }
            catch(System.ArgumentException e)
            {
                Console.WriteLine($"{e.Message}");
                return 0;
            }
        }
        private bool isEmpty() // проврека, пустая ли очередь
        {
            if (QueueService.Count == 0) return true;
            else return false;
        }
        public void processing(Time now)
        {
            for (int num_spec = 0; num_spec < num; num_spec++) // отмечает свободных специалистов
            {
                if (spec[num_spec].Open(now))
                    spec[num_spec].OpenStat(now);
            }
            if (!isEmpty())
            {
                for (int num_queue = 0; num_queue < QueueService.Count; num_queue++) // проход по очереди
                {
                    for (int num_spec = 0; num_spec < num; num_spec++) // поиск свободного специалиста
                    {
                        if (spec[num_spec].Open(now))
                        {
                            WaitingTime.Add(Time.minus(now, QueueService[num_queue].Get_start_time())); // запоминание времени ожидания
                            spec[num_spec].Set(now, QueueService[num_queue].get_ID()); // утсановка времени, когда осовбодиться специалист
                            QueueService.Remove(QueueService[num_queue]); // удаление из очереди
                            break; // дальше свободного специалиста искать не нужно
                        }
                    }
                }
            }
        }
        public List<Time> WaitngList() { return WaitingTime; }
        public void print()
        {
            Console.Write("\n////// Type service: " + type_ser + " //////////////////////////////////////////////////");
            for (int num_spec = 0; num_spec < num; num_spec++)
            {
                Console.Write("\nSpecialist " + (num_spec + 1));
                spec[num_spec].InfoService();
            }
        }
    }
}
