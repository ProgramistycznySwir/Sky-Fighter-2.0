using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogConsole_irl : MonoBehaviour
{
    public TMPro.TextMeshPro console;
    protected int howManyLogs = 0; // how many logs soo far
    protected List<Log> listOfLogs = new List<Log>();
    protected System.Collections.Generic.Stack<Log> stos;

    public float commonLogLifespan = 10f;

    // Start is called before the first frame update
    void Start()
    {
        NewLog("LogConsole: online!", 0.1f);
        //UpdateConsole();
    }

    // Update is called once per frame
    void Update()
    {
        //NewLog("How's with optimalisation?");


        //Queue<int> deathList = new Queue<int>();
        List<int> deathList = new List<int>();

        foreach (Log log in listOfLogs)
        {
            //Debug.Log("Examining log nr." + log.ID + "/" + log.lifespan + "/" + log.timeStamp);

            if (log.CheckAge())
            {
                //deathList.Enqueue(listOfLogs.IndexOf(log));
                deathList.Add(listOfLogs.IndexOf(log));
                //listOfLogs.Remove(log); Debug.Log("Should be dead >" + log.ID);// listOfLogs.RemoveAt(log.ID);
            }

            //Debug.Log("Successfull examination!");
        }

        for(int a = deathList.Count - 1; a >= 0; a--)
        {
            listOfLogs.RemoveAt(deathList[a]);
        }

        //foreach (int index in deathList)
        //{
        //    Debug.Log("Index: " + index);
        //    listOfLogs.RemoveAt(index);
        //}

        //listOfLogs.Sort();

        UpdateConsole();
    }

    /// <summary>
    /// Method for making logs into LogConsole
    /// </summary>
    /// <param name="text"> Message of this log</param>
    public void NewLog(string text)
    {
        Log newLog = new Log(text, howManyLogs, commonLogLifespan);

        listOfLogs.Add(newLog);
        howManyLogs++;
    }
    /// <summary>
    /// Method for making logs into LogConsole with custom lifespan
    /// </summary>
    /// <param name="text"> Message of this log</param>
    /// <param name="lifespan"> Time in seconds which this log is "alive"</param>
    public void NewLog(string text, float lifespan)
    {
        Log newLog = new Log(text, howManyLogs, lifespan);

        listOfLogs.Add(newLog);
        howManyLogs++;
    }

    protected string masterText;
    public void UpdateConsole()
    {
        masterText = "";


        foreach (Log log in listOfLogs)
            masterText += $">{log.text}\n";

        console.text = masterText;
    }

    public void CoolConsoleStartupMessage()
    {
        StartCoroutine("Startup");
    }

    protected IEnumerator Startup()
    {
        NewLog("Welcome Captain.", 10.8f);//
        NewLog("Starting booting sequence:", 10.8f);//
        yield return new WaitForSeconds(0.1f);
        NewLog("Reactor: booting...", 1f);
        yield return new WaitForSeconds(1f);
        NewLog("Reactor: online", 9.7f);//

        yield return new WaitForSeconds(0.1f);
        NewLog("Life Support: booting...", 0.4f);
        yield return new WaitForSeconds(0.4f);
        NewLog("Life Support: online...", 9.2f);//

        yield return new WaitForSeconds(0.1f);
        NewLog("Engines: booting...", 1.3f);
        yield return new WaitForSeconds(0.1f);
        NewLog("Sensors: booting...", 0.2f);
        yield return new WaitForSeconds(0.2f);
        NewLog("Sensors: online...", 8.8f);//
        yield return new WaitForSeconds(1f);
        NewLog("Engines: online...", 7.8f);//

        yield return new WaitForSeconds(0.1f);
        NewLog("Shield: booting...", 2.6f);

        yield return new WaitForSeconds(0.1f);
        NewLog("Weapons: booting...", 0.5f);
        yield return new WaitForSeconds(0.5f);
        NewLog("Weapons: online...", 7.1f);//

        yield return new WaitForSeconds(2f);
        NewLog("Shield: online...", 5.1f);//

        yield return new WaitForSeconds(0.1f);
        NewLog("Startup and checkup finished successfully: all systems nominal", 5f);//
        float startupTime = Random.Range(5.7f, 5.9f);  //5.8sec is full time
        NewLog("Startup sequence finished in: " + System.Convert.ToString(System.Math.Round(startupTime, 2)) + " seconds", 5f);//
    }

    protected struct Log
    {
        public int ID;
        public float timeStamp;

        public string text;
        public float lifespan;

        public Log(string text, int logID, float lifespan)
        {
            ID = logID;
            timeStamp = Time.time;
            this.text = text;
            this.lifespan = lifespan;
            //Debug.Log("Created new log: (" + ID + "/\"" + this.text + "\"/" + timeStamp + ")");
        }

        /// <summary>
        /// Checks the age of a log
        /// </summary>
        /// <returns> If dead</returns>
        public bool CheckAge()
        { return (timeStamp + lifespan < Time.time); }
    }
}
