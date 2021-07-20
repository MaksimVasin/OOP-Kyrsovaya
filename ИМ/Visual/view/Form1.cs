using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QueuingSystem.model;


namespace Visual
{
    public partial class Form : System.Windows.Forms.Form
    {
        private static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer(); // таймер
        private Time now, end_work; // время сейчас и конец рабочего дня
        public Service pasport, viza, reg; // виды сервисов
        private int frequency, count_clients,ID; // частота, количесвто клиентов и айди
        private Random rnd;
        private int speed; // скорость моделирования
        public Form()
        {
            InitializeComponent();
            Init();
            myTimer.Tick += new EventHandler(CMO_One_iter);
        }
        private void CMO_One_iter(object sender, EventArgs e) // одна итерация системы (одна минута)
        {
            int type_serv;
            if (rnd.Next() % frequency == 0) // создание нового клиента // рандомно
            {
                for (int cl = 0; cl < count_clients; cl++)
                {
                    type_serv = Service.Rand_t_s(10, 40, 50, rnd.Next()); // процентное соотношение популярности 
                    Client cur_client = new Client(now, ID++, (Type_service)type_serv);
                    if (cur_client.get_type() == (Type_service)0)
                        pasport.AddQueue(cur_client); // добавление в очередь на получение паспорта
                    else if (cur_client.get_type() == (Type_service)1)
                        viza.AddQueue(cur_client);  // добавление в очередь на визу
                    else if (cur_client.get_type() == (Type_service)2)
                        reg.AddQueue(cur_client);   // добавление в очередь на регистарцию
                }
            }
            // изминение частоты посещаемости взависисмости от времени
            if (Time.equally(new Time(7, 0), now)) frequency = 3;
            if (Time.equally(new Time(8, 0), now)) frequency = 2;
            if (Time.equally(new Time(10, 0), now)) frequency = 3;
            if (Time.equally(new Time(14, 0), now)) frequency = 4;
            if (Time.equally(new Time(15, 0), now)) frequency = 3;
            if (Time.equally(new Time(16, 0), now)) frequency = 2;
            if (Time.equally(new Time(18, 0), now)) frequency = 4;

            pasport.processing(now);
            viza.processing(now);
            reg.processing(now);

            ///////////////////////SHOW
            Show_time(now);
            Show_service();
            Show_open_time();
            Show_queue();

            now = now.add(new Time(0,1));
            if (Time.equally(now, end_work))
            {
                ButtonPause.Enabled = false;
                ButtonStart.Enabled = false;
                myTimer.Stop();
                stop();
            }
        }
        private void Show_open_time() // отображения времени открытия 
        {
            if (!pasport.spec[0].FreeSpec()) open_time1.Text = pasport.spec[0].Open_time();
            else open_time1.Text = "open";
            if (!pasport.spec[1].FreeSpec()) open_time2.Text = pasport.spec[1].Open_time();
            else open_time2.Text = "open";
            if (!pasport.spec[2].FreeSpec()) open_time3.Text = pasport.spec[2].Open_time();
            else open_time3.Text = "open";
            if (!pasport.spec[3].FreeSpec()) open_time4.Text = pasport.spec[3].Open_time();
            else open_time4.Text = "open";
            if (!pasport.spec[4].FreeSpec()) open_time5.Text = pasport.spec[4].Open_time();
            else open_time5.Text = "open";

            if (!viza.spec[0].FreeSpec()) open_time6.Text = viza.spec[0].Open_time();
            else open_time6.Text = "open";
            if (!viza.spec[1].FreeSpec()) open_time7.Text = viza.spec[1].Open_time();
            else open_time7.Text = "open";
            if (!viza.spec[2].FreeSpec()) open_time8.Text = viza.spec[2].Open_time();
            else open_time8.Text = "open";
            if (!viza.spec[3].FreeSpec()) open_time9.Text = viza.spec[3].Open_time();
            else open_time9.Text = "open";
            if (!viza.spec[4].FreeSpec()) open_time10.Text = viza.spec[4].Open_time();
            else open_time10.Text = "open";

            if (!reg.spec[0].FreeSpec()) open_time11.Text = reg.spec[0].Open_time();
            else open_time11.Text = "open";
            if (!reg.spec[1].FreeSpec()) open_time12.Text = reg.spec[1].Open_time();
            else open_time12.Text = "open";
            if (!reg.spec[2].FreeSpec()) open_time13.Text = reg.spec[2].Open_time();
            else open_time13.Text = "open";
            if (!reg.spec[3].FreeSpec()) open_time14.Text = reg.spec[3].Open_time();
            else open_time14.Text = "open";
            if (!reg.spec[4].FreeSpec()) open_time15.Text = reg.spec[4].Open_time();
            else open_time15.Text = "open";
        }
        private void Show_queue() // отображение очереди
        {
            Queue1.Clear();
            for (int i = 0; i < pasport.Get_queue().Count; i++)
            {
                Queue1.AppendText(pasport.Get_queue()[i].get_ID() + "\n");
            }
            Queue2.Clear();
            for (int i = 0; i < viza.Get_queue().Count; i++)
            {
                Queue2.AppendText(viza.Get_queue()[i].get_ID() + "\n");
            }
            Queue3.Clear();
            for (int i = 0; i < reg.Get_queue().Count; i++)
            {
                Queue3.AppendText(reg.Get_queue()[i].get_ID() + "\n");
            }
            WaitngTimeList1.Clear();
            WaitngTimeList1.AppendText(Time.average(pasport.WaitngList(), 10).Get_time_string());
            WaitngTimeList2.Clear();
            WaitngTimeList2.AppendText(Time.average(viza.WaitngList(), 10).Get_time_string());
            WaitngTimeList3.Clear();
            WaitngTimeList3.AppendText(Time.average(reg.WaitngList(), 10).Get_time_string());
            /*WaitngTimeList1.Clear();
            for (int i = pasport.WaitngList().Count - 1; i >= 0; i--)
            {
                WaitngTimeList1.AppendText(pasport.WaitngList()[i].Get_time_string() + "\n");
            }
            WaitngTimeList2.Clear();
            for (int i = viza.WaitngList().Count - 1; i >= 0; i--)
            {
                WaitngTimeList2.AppendText(viza.WaitngList()[i].Get_time_string() + "\n");
            }
            WaitngTimeList3.Clear();
            for (int i = reg.WaitngList().Count - 1; i >= 0; i--)
            {
                WaitngTimeList3.AppendText(reg.WaitngList()[i].Get_time_string() + "\n");
            }*/
        }
        private void Show_service() // отображения сервисов
        {
            if (!pasport.spec[0].FreeSpec()) { Spec1.Text = pasport.spec[0].ID_Client().ToString(); Spec1.BackColor = Color.LightCoral; }
            else { Spec1.Clear(); Spec1.BackColor = Color.PaleGreen; };
            if (!pasport.spec[1].FreeSpec()) { Spec2.Text = pasport.spec[1].ID_Client().ToString(); Spec2.BackColor = Color.LightCoral; }
            else { Spec2.Clear(); Spec2.BackColor = Color.PaleGreen; };
            if (!pasport.spec[2].FreeSpec()) { Spec3.Text = pasport.spec[2].ID_Client().ToString(); Spec3.BackColor = Color.LightCoral; }
            else { Spec3.Clear(); Spec3.BackColor = Color.PaleGreen; }
            if (!pasport.spec[3].FreeSpec()) { Spec4.Text = pasport.spec[3].ID_Client().ToString(); Spec4.BackColor = Color.LightCoral; }
            else { Spec4.Clear(); Spec4.BackColor = Color.PaleGreen; }
            if (!pasport.spec[4].FreeSpec()) { Spec5.Text = pasport.spec[4].ID_Client().ToString(); Spec5.BackColor = Color.LightCoral; }
            else { Spec5.Clear(); Spec5.BackColor = Color.PaleGreen; }

            if (!viza.spec[0].FreeSpec()) { Spec6.Text = viza.spec[0].ID_Client().ToString(); Spec6.BackColor = Color.LightCoral; }
            else { Spec6.Clear(); Spec6.BackColor = Color.PaleGreen; }
            if (!viza.spec[1].FreeSpec()) { Spec7.Text = viza.spec[1].ID_Client().ToString(); Spec7.BackColor = Color.LightCoral; }
            else { Spec7.Clear(); Spec7.BackColor = Color.PaleGreen; }
            if (!viza.spec[2].FreeSpec()) { Spec8.Text = viza.spec[2].ID_Client().ToString(); Spec8.BackColor = Color.LightCoral; }
            else { Spec8.Clear(); Spec8.BackColor = Color.PaleGreen; }
            if (!viza.spec[3].FreeSpec()) { Spec9.Text = viza.spec[3].ID_Client().ToString(); Spec9.BackColor = Color.LightCoral; }
            else { Spec9.Clear(); Spec9.BackColor = Color.PaleGreen; }
            if (!viza.spec[4].FreeSpec()) { Spec10.Text = viza.spec[4].ID_Client().ToString(); Spec10.BackColor = Color.LightCoral; }
            else { Spec10.Clear(); Spec10.BackColor = Color.PaleGreen; }

            if (!reg.spec[0].FreeSpec()) { Spec11.Text = reg.spec[0].ID_Client().ToString(); Spec11.BackColor = Color.LightCoral; }
            else { Spec11.Clear(); Spec11.BackColor = Color.PaleGreen; }
            if (!reg.spec[1].FreeSpec()) { Spec12.Text = reg.spec[1].ID_Client().ToString(); Spec12.BackColor = Color.LightCoral; }
            else { Spec12.Clear(); Spec12.BackColor = Color.PaleGreen; }
            if (!reg.spec[2].FreeSpec()) { Spec13.Text = reg.spec[2].ID_Client().ToString(); Spec13.BackColor = Color.LightCoral; }
            else { Spec13.Clear(); Spec13.BackColor = Color.PaleGreen; }
            if (!reg.spec[3].FreeSpec()) { Spec14.Text = reg.spec[3].ID_Client().ToString(); Spec14.BackColor = Color.LightCoral; }
            else { Spec14.Clear(); Spec14.BackColor = Color.PaleGreen; }
            if (!reg.spec[4].FreeSpec()) { Spec15.Text = reg.spec[4].ID_Client().ToString(); Spec15.BackColor = Color.LightCoral; }
            else { Spec15.Clear(); Spec15.BackColor = Color.PaleGreen; }
        }
        private void Init() // инициализация переменных
        {
            this.now = new Time(6, 0); // время сейчас
            this.end_work = new Time(20, 30); // время окончания работы
            this.pasport = new Service(5, now, (Type_service)0);
            this.viza = new Service(5, now, (Type_service)1);
            this.reg = new Service(5, now, (Type_service)2);
            this.frequency = 4; // когда повышается этот параметр, клиентов становиться меньше
            this.count_clients = 2; // количество клиетов за одну итерацию (в минуту)
            this.rnd = new Random();
            this.ID = 100; // начальный айди
            this.speed = 1000; // скорость симуояции
            myTimer.Interval = this.speed;
        }
        private void Show_time(Time time)
        {
            time_now.Text = time.Get_time_string();
        }
        private void pause_click(object sender, EventArgs e)
        {
            ButtonPause.Enabled = false;
            ButtonStart.Enabled = true;
            myTimer.Stop();
        }
        private void stop() // остановка
        {
            ButtonPause.Enabled = false;
            ButtonStart.Enabled = false;
            ButtonEnd.Enabled = false;
            myTimer.Stop();
            Form3 a = new Form3(ref pasport, ref viza, ref reg);
            a.ShowDialog();
            this.Close();
        }
        private void stop_click(object sender, EventArgs e) // завершение работы программы
        {
            stop();
        }

        private void Speed_add_Click(object sender, EventArgs e) // увеличение скорости симуляции
        {
            Speed_min.Enabled = true;
            if (speed >= 30) speed /= 2;
            else Speed_add.Enabled = false;
            myTimer.Interval = speed;
        }

        private void Speed_min_Click(object sender, EventArgs e) // уменьшение скорости симуляции
        {
            Speed_add.Enabled = true;
            if (speed <= 4000) speed *= 2;
            else Speed_min.Enabled = false;
            myTimer.Interval = speed;
        }

        private void flow_min_Click(object sender, EventArgs e) // уменьшение потока
        {
            Flow_add.Enabled = true;
            if (count_clients != 1) count_clients--;
            else Flow_min.Enabled = false;
        }

        private void Flow_add_Click(object sender, EventArgs e) // увеличение потока
        {
            Flow_min.Enabled = true;
            if (count_clients != 10) count_clients++;
            else Flow_add.Enabled = false;
        }
        private void Clear1_Click(object sender, EventArgs e) // очистка первой очереди
        {
            pasport.Clear();
        }
        private void Clear2_Click(object sender, EventArgs e) // очистка второй очереди
        {
            viza.Clear();
        }

        private void Clear3_Click(object sender, EventArgs e) // очистка третей очереди
        {
            reg.Clear();
        }
        private void button1_Click(object sender, EventArgs e) // старт
        {
            ButtonPause.Enabled = true;
            ButtonStart.Enabled = false;
            myTimer.Start();
        }
    }
}
