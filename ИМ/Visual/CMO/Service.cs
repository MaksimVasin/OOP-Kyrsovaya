using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueuingSystem
{
    public class Program
    {
        /*static void Main(string[] args)
        {
            CMO a = new CMO();
            a.Start();
        }*/
    }
    public class Time
    {
        private int hours, minutes;
        public Time()
        {
            this.hours = 0;
            this.minutes = 0;
        }
        public int Get_hours() {return hours;}
        public int Get_minutes() { return minutes; }
        public static bool equally(Time a, Time b)
        {
            if (a.hours == b.hours)
            {
                if (a.minutes == b.minutes)
                {
                    return true;
                }
            }
            return false;
        }
        /*public static Time min_time(Time[] time, int size)
        {
            Time min = new Time(24, 60);
            foreach(Time cur in time)
            {
                if (cur.Get_hours() < min.Get_hours()) min = cur;
                else if (cur.Get_hours() == min.Get_hours())
                {
                    if (cur.Get_minutes() < min.Get_minutes()) min = cur;
                }
            }
            return min;
        }*/
        public Time(int hours, int minutes)
        {
            try
            {
                if (hours > 24 || minutes > 60 || hours < 0 || minutes < 0) throw new ArgumentException("Incorrect input");
                this.hours = hours;
                this.minutes = minutes;
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }      
        public Time add(Time b) // ДОБАВИТЬ ИСКЛЧБЮЕНИ МБ больШЕ 24 часов
        {
            Time sum = new Time();
            sum.hours = hours + b.hours;
            sum.minutes = minutes + b.minutes;
            if (sum.minutes > 60)
            {
                sum.hours++;
                sum.minutes -= 60;
            }
            return sum;
        }
        public bool _greater_(Time B)
        {
            if (hours == B.hours)
            {
                if (minutes >= B.minutes)
                    return true;
                else
                    return false;
            }
            else if (hours > B.hours)
            {
                return true;
            }
            else 
            {
                return false;
            }       
        }
        /*public Time add(int h, int m) // ДОБАВИТЬ ИСКЛЧБЮЕНИ МБ больШЕ 24 часов
        {
            try
            {
                if (h < 0 || m < 0) throw new ArgumentException("Incorrect input");
                Time sum = new Time();
                sum.hours = hours + h;
                sum.minutes = minutes + m;
                if (sum.minutes > 60)
                {
                    sum.hours++;
                    sum.minutes -= 60;
                }
                return sum;
            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine($"{e.Message}");
                return null;
            }
        }
        public Time minus(Time b)
        {
            try
            {
                if (hours < b.hours) throw new ArgumentException("Incorrect input"); 
                if (hours == b.hours && minutes < b.minutes) throw new ArgumentException("Incorrect input");
                Time dif = new Time();
                dif.minutes = minutes - b.minutes;
                dif.hours = hours - b.hours;
                if (dif.minutes < 0)
                {
                    dif.hours--;
                    dif.minutes += 60;
                }
                return dif;

            }
            catch (System.ArgumentException e)
            {
                Console.WriteLine($"{e.Message}");
                return null;
            }
        }*/
        public void Print()
        {
            if (hours < 10) Console.Write("0{0}", hours);
            else Console.Write("{0}", hours);
            if (minutes < 10) Console.Write(":0{0}", minutes);
            else Console.Write(":{0}", minutes);
        }

    }
    public enum Type_service
    {
        pasport,
        viza,
        registr
    }
    public enum Status_specialist
    {
        free,
        busy
    }
    public class Specialist // one window
    {
        private Status_specialist stat;
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
        public void OpenStat(Time now)
        {
            this.stat = Status_specialist.free;
            this.ID_client = 0;
            this.service_time = new Time(0, 0);
            this.open_time = now;
        }
        public void Set(Time arrival_time, int ID)
        {
            this.stat = Status_specialist.busy;
            Random rnd = new Random();
            this.ID_client = ID;
            this.service_time = new Time(0, (rnd.Next() % 20 + 5));
            this.open_time = arrival_time.add(this.service_time);
        }
        public void Set(Time arrival_time, Time service_time, int ID)
        {
            Random rnd = new Random();
            this.ID_client = ID;
            this.open_time = arrival_time.add(service_time);
        }
        public bool Open(Time now_time)
        {
            if (now_time._greater_(open_time))
            {
                this.stat = Status_specialist.free;
                return true; // свободна ли касса
            }
            else
            {
                this.stat = Status_specialist.busy;
                return false;
            }
        }
        public void InfoService()
        {
            if (this.stat == Status_specialist.free)
            {
                Console.Write(" free");
            }
            else
            {
                Console.Write(" ID = " + ID_client);
                Console.Write(" opening v: "); open_time.Print();
                Console.Write(" service = "); service_time.Print();
            }
        }
    }
    public class Service
    {
        private Type_service type_ser;
        private int num;
        Specialist[] spec;
        List<Client> QueueService;
        public Service(int num_of_spec, Time now, Type_service ser)
        {
            this.num = num_of_spec;
            this.spec = new Specialist[this.num];
            this.QueueService = new List<Client>();
            this.type_ser = ser;
            for (int i = 0; i < this.num; i++)
                spec[i] = new Specialist(now, 0);
        }
        private void PrintType() { Console.WriteLine(type_ser); }
        public void AddQueue(Client cur)
        {
            QueueService.Add(cur);
        }
        private bool isEmpty()
        {
            if (QueueService.Count == 0) return true;
            else return false;
        }
        public void processing(Time now)
        {
            if (!isEmpty())
            {
                for (int num_queue = 0; num_queue < QueueService.Count; num_queue++)
                {
                    for (int num_spec = 0; num_spec < num; num_spec++)
                    {
                        if (spec[num_spec].Open(now))
                        {
                            spec[num_spec].Set(now, QueueService[num_queue].get_ID());
                            QueueService.Remove(QueueService[num_queue]);
                            break;
                        }
                    }
                }
            }
            for (int num_spec = 0; num_spec < num; num_spec++)
            {
                if (spec[num_spec].Open(now))
                    spec[num_spec].OpenStat(now);
            }
        }
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
    public class CMO
    {
        public void Start()
        {
            Time now = new Time(6, 0); //
            Time end_work = new Time(21, 00); //
            Service pasport = new Service(4, now, (Type_service)0);
            Service viza = new Service(5, now, (Type_service)1);
            Service reg = new Service(2, now,(Type_service)2);

            // ПАРАМЕТРЫ, ПОЗВОЛЯЮЩИЕ РЕГУЛИРОВАТЬ ПОСЕЩАЕМОСТЬ
            int frequency = 2; // когда повышается этот параметр, клиентов становиться меньше
            int count_clients = 2; // количество клиетов за одну итерацию (в минуту)

            Random rnd = new Random();
            int ID = 100;
            while (!Time.equally(now, end_work)) // пока время сейчас не равно концу рабочего дня
            {
                if (rnd.Next() % frequency == 0) // создание нового клиента
                {
                    for (int cl = 0; cl < count_clients; cl++)
                    {
                        Client cur_client = new Client(now, 1, ID++, (Type_service)(rnd.Next() % 3));
                        if (cur_client.get_type() == (Type_service)0)
                            pasport.AddQueue(cur_client);
                        else if (cur_client.get_type() == (Type_service)1)
                            viza.AddQueue(cur_client);
                        else if (cur_client.get_type() == (Type_service)2)
                            reg.AddQueue(cur_client);
                    }
                }

                // изминение частоты посещаемости
                if (Time.equally(new Time(7, 0), now)) frequency--;
                if (Time.equally(new Time(12, 0), now)) frequency++;
                if (Time.equally(new Time(13, 0), now)) frequency++;
                if (Time.equally(new Time(14, 0), now)) frequency++;
                if (Time.equally(new Time(15, 0), now)) frequency++;

                if (Time.equally(new Time(16, 0), now)) frequency--;
                if (Time.equally(new Time(18, 0), now)) frequency--;
                if (Time.equally(new Time(20, 0), now)) frequency--;



                viza.processing(now);
                pasport.processing(now);
                reg.processing(now);

                viza.print();
                pasport.print();
                reg.print();

                Console.WriteLine();
                now = now.add(new Time(0, 1));
                Console.Write("\nTime:"); now.Print();
                Thread.Sleep(100); // задержка
                Console.Clear();
            }
        }
    }
}
