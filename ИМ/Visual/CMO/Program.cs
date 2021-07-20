using System;
using System.Collections.Generic;

/*namespace QueuingSystem
{
    *//*class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }*//*
    public class Сlient
    {

        public Сlient()
        {
            Random rnd = new Random();
            service = rnd.Next() % 2;
            time = rnd.Next() % 15;
        }
        public Queue TypeService;
        //private string name;
        public int service;
        public int time;
        public int ReturnTime() { return time; }
        public int ReturnService() { return service; }
        public Queue ReturnTypeService() { return TypeService; }
    } 
    public class Queue
    {
        public Queue(string name, int qLong, int numOfSpec)
        {
            this.qName = name;
            this.qLong = qLong;
            this.maxnumOfSpec = numOfSpec;
            this.numOfSpec = numOfSpec;
        }
        public int time;
        public int qLong, numOfSpec, maxnumOfSpec;
        public string qName;
        public void newClient()
        {
            Random rnd = new Random();
            time += rnd.Next() % 10;
            numOfSpec--;
        }
        public LinkedList<int> Q = new LinkedList<int>();
        public void push_back()
        {
            Q.AddLast(1);
        }
        *//*int return_front()
        {
            Q.Get
        }*//*
        public bool is_Emply()
        {
            if (Q.Count == 0) return true;
            else return false;
        }
        public void pop_front()
        {
            Q.RemoveFirst();
        }
    }
    public class Logic
    {
        //public int Alltime = 0;
        public int time;
        public void Metod()
        {
            Queue Q1 = new Queue("pasport", 10, 5);
            Queue Q2 = new Queue("avtobus", 5, 10);
            //Сlient A = new Сlient();

            for (int i = 0; i < 10; i++)
            {

                Сlient Aclient = new Сlient();
                switch (Aclient.ReturnService())
                {
                    case 1:
                        Q1.push_back();
                        Q1.newClient();
                        break;
                    case 2:
                        Q2.push_back();
                        Q2.newClient();
                        break;
                }
            }
        }
    }
}*/
