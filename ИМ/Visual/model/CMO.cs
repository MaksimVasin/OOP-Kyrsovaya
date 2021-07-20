using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace QueuingSystem.model
{
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
    public class CMO
    {
        public void Start()
        {
            Time now = new Time(6, 0); //
            Time end_work = new Time(7, 0); //
            Service pasport = new Service(5, now, (Type_service)0);
            Service viza = new Service(5, now, (Type_service)1);
            Service reg = new Service(5, now, (Type_service)2);
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
                        Client cur_client = new Client(now, ID++, (Type_service)(rnd.Next() % 3));
                        if (cur_client.get_type() == (Type_service)0)
                            pasport.AddQueue(cur_client); // добавление в очередь на получение паспорта
                        else if (cur_client.get_type() == (Type_service)1)
                            viza.AddQueue(cur_client); // добавление в очередь на визу
                        else if (cur_client.get_type() == (Type_service)2)
                            reg.AddQueue(cur_client); // добавление в очередь на регистрацию
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
