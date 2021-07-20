using System;
using System.Collections.Generic;
using System.Text;

namespace QueuingSystem.model
{
    public class Time
    {
        private int hours, minutes; // часы, минуты
        public Time()
        {
            this.hours = 0;
            this.minutes = 0;
        }
        public String Get_time_string() // получить время в строковом формате
        {
            String str_time;
            if (hours < 10) str_time = "0" + hours.ToString() + ":";
            else str_time = hours.ToString() + ":";
            if (minutes < 10) str_time += "0" + minutes.ToString();
            else str_time += minutes.ToString();
            return str_time;
        }
        public int Get_hours() { return hours; }
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
        public Time add(Time b) // добавление времени
        {
            Time sum = new Time();
            sum.hours = hours + b.hours;
            sum.minutes = minutes + b.minutes;
            if (sum.minutes >= 60)
            {
                sum.hours++;
                sum.minutes -= 60;
            }
            return sum;
        }
        public bool _greater_(Time B) // проверка больше ли одно число другого
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
        static public Time average(List<Time> time, int interval) // среднее время листа
        {
            int inter = interval;
            if (time.Count != 0)
            {
                int all_time = 0;
                for (int i = time.Count-1; (i > 0) && (interval > 0); i--) 
                {
                    all_time += in_int(time[i]);
                    interval--;
                }
                if (interval != 0) return in_time(all_time / time.Count);
                else return in_time(all_time / inter);
            }
            else return new Time(0, 0);
        }
        static private int in_int(Time time) // получить время полнотью в минутах
        {
            return time.Get_hours() * 60 + time.Get_minutes();
        }
        static private Time in_time(int time) // перевод времени только в минутах в часы с минутами
        {
            return new Time(time / 60, time % 60);
        }
        static public Time minus(Time a, Time b) // вычитание времени
        {
            try
            {
                if (a.hours < b.hours) throw new ArgumentException("Incorrect input"); 
                if (a.hours == b.hours && a.minutes < b.minutes) throw new ArgumentException("Incorrect input");
                Time dif = new Time();
                dif.minutes = a.minutes - b.minutes;
                dif.hours = a.hours - b.hours;
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
        }
        public void Print()
        {
            if (hours < 10) Console.Write("0{0}", hours);
            else Console.Write("{0}", hours);
            if (minutes < 10) Console.Write(":0{0}", minutes);
            else Console.Write(":{0}", minutes);
        }
    }
}
