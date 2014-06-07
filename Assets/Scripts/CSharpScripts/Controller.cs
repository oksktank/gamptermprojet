using UnityEngine;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System;
using System.Timers;
using System.Threading;


public class Controller : MonoBehaviour {
 
	static SerialPort serialport1 = new SerialPort();
    static SerialPort serialport2 = new SerialPort();

    string[] portsarray;
    int prev_portsarray_Length;
	
	string[] temp_line = new string[2];
	string[] temp_line2 = new string[2];
	
    public string direction1;
    public string direction2;
	
	public int cnt = 0;
	public int cnt2 = 0;
	public int port1 = 0, port2 = 0;
	string imsi = "XX_0";
	
	int check_num = 0 ;
	int prev_check_num = 0;
	int check_err_cnt = 0;
	string str_check_num ;

	
	string imsi2 = "XX_0";
	
	int check_num2 = 0 ;
	int prev_check_num2 = 0;
	int check_err_cnt2 = 0;
	string str_check_num2 ;

	public bool P1_connect ;
	public bool P2_connect ;
	
	// Use this for initialization
	void Start () {
		port1 = 0;
		port2 = 0;
		P1_connect = false;
		P2_connect = false;
		portsarray = SerialPort.GetPortNames();
		
		prev_portsarray_Length = portsarray.Length;
		Ports_setting(portsarray.Length);
		imsi = "XX_0";
		prev_check_num = check_num;
		prev_check_num2 = check_num2;

		
		imsi2 = "XX_0";

    }
	
	void OnApplicationQuit()
	{
		if(serialport1 != null && serialport1.IsOpen)
		{
		    serialport1.Close();
			serialport1 = null;
		}
		if(serialport2 != null && serialport2.IsOpen)
		{
		    serialport2.Close();
			serialport2 = null;
		}
		
	}
	
	void Reset()
	{
		
		if(serialport1 != null && serialport1.IsOpen)
		{
			serialport1.Dispose();
			serialport1.Close();
		}
			
		if(serialport2 != null && serialport2.IsOpen)
		{
			serialport2.Dispose();
			serialport2.Close();
		}
		
		serialport1 = null;
		serialport2 = null;
		
    	direction1 = "EE";
    	direction2 = "EE";

		
		serialport1 = new SerialPort();
    	serialport2 = new SerialPort();
   		 
		cnt = 0;
		cnt2 = 0;
		port1 = 0; port2 = 0;
		
		portsarray = SerialPort.GetPortNames();
       	Ports_setting(portsarray.Length);
		
	}
	
    void Update()
    {
		portsarray = SerialPort.GetPortNames();
		
		if(portsarray.Length > prev_portsarray_Length)
		{
			Reset ();
			
	 		prev_portsarray_Length = portsarray.Length;
		}
		else
		{
			prev_portsarray_Length = portsarray.Length;
		}

		
		if(serialport1 != null && serialport1.IsOpen)
		{
			serialport1_datareceived();
			P1_connect = true;
		}
	
		if(serialport2 != null && serialport2.IsOpen)
		{
       		serialport2_datareceived();
			P2_connect = true;
		}

		if( temp_line[1] != null)
		{
			check_num = int.Parse(temp_line[1]);
		
			if(prev_check_num == check_num && serialport1 != null)
			{
				check_err_cnt++;
			}
			else{
				check_err_cnt = 0;
			}
		
			if(check_err_cnt > 30)
			{
	  			if(serialport1 != null)
				{
					serialport1.Dispose();
					serialport1.Close();
				}
				
				serialport1 = null;
			
	    		direction1 = "EE";
						
				cnt = 0;
				port1 = 0;
		
				P1_connect = false;
				prev_portsarray_Length = portsarray.Length;
				check_err_cnt = 0;
			}
		}
	
				prev_check_num = check_num;
		
			
			if(temp_line2[1] != null)
				{
				check_num2 = int.Parse(temp_line2[1]);
			
				if(prev_check_num2 == check_num2 && serialport2 != null)
				{
					check_err_cnt2++;
				}
				else
				{
					check_err_cnt2 = 0;
				}
	 		
				if(check_err_cnt2 > 30)
				{
		  			
					if(serialport2 != null)
					{
						serialport2.Dispose();
						serialport2.Close();
					}
				
					serialport2 = null;
		    		direction2 = "EE";
							
					cnt2 = 0;
					port2 = 0;
			
				    P2_connect = false;
					prev_portsarray_Length = portsarray.Length;
					check_err_cnt2 = 0;
					}
				}
				prev_check_num2 = check_num2;
		
		
    }
	
	void Renew_port_instance(int port)
	{
		if(port == 1 || port == 0)
		{
			if(serialport1.IsOpen)
			{
				serialport1.Dispose();
				serialport1.Close();
			}
			serialport1 = null;
			serialport1 = new SerialPort();
		}
		if(port == 2 || port == 0)
		{
			if(serialport2.IsOpen)
			{
				serialport2.Dispose();
				serialport2.Close();
			}
			
			serialport2 = null;
			serialport2 = new SerialPort();
		}
				
	}
	
    void Ports_setting(int portsize)
    {	
		int i, bcnt = 0, flag = 0;
	
		for( i = 0 ; i < portsize ; i++)
		{
			if(port1 == 0 && serialport1.PortName != null && 
				serialport1 != null && !serialport1.IsOpen)
			{
				serialport1.PortName = portsarray[i];
				serialport1.BaudRate = 9600;
				serialport1.ReadTimeout = 20;
				serialport1.Open();
			}
		   
			else if(port2 == 0 && serialport2.PortName != null && 
				serialport2 != null&& !serialport2.IsOpen)
			{
				serialport2.PortName = portsarray[i];
				serialport2.BaudRate = 9600;
				serialport2.ReadTimeout = 20;
				serialport2.Open();
			}

			bcnt = 0;
				
			while(true)
				{
					if(port1 == 0)
					{
						if(serialport1.IsOpen)
						{
							serialport1_datareceived();
						}
					
						if(direction1 == "WW" || direction1 == "FW" || direction1 == "BW" || direction1 == "WL" || direction1 == "WR"
							|| direction1 == "FL" || direction1 == "FR" || direction1 == "BL" || direction1 == "BR"|| direction1 == "EE")
						{
			
							//check_port(1);
							port1 = 1;
							//P1_connect = true;
							break;
						}
						else{
							bcnt++;
							if(bcnt == 3)
							{
								serialport1.Dispose();
								serialport1.Close ();
								break;
							}
						}
					}
					else if(port2 == 0){
						if(serialport2.IsOpen)
						{
							serialport2_datareceived();
						}
						if(direction2 == "WW" || direction2 == "FW" || direction2 == "BW" || direction2 == "WL" || direction2 == "WR"
							|| direction2 == "FL" || direction2 == "FR" || direction2 == "BL" || direction2 == "BR" || direction2 == "EE")
						{
							//check_port(2);
							port2 = 1;
							//P2_connect = true;
							break;
						}
						else{
							bcnt++;
							if(bcnt == 3)
							{
								serialport2.Dispose ();
								serialport2.Close ();
								break;
							}
						}
					}
				}
			
			if(port2 == 1) break;
		}
		
		if(port1 == 0 && port2 == 0)
		{
			P1_connect = false;
			P2_connect = false;
		}
		if((port1 == 1 && port2 == 0) || (port1 == 0 && port2 == 1))
		{
			if(serialport1 != null)
			{
				serialport1_datareceived();
				//confirm_num = check_num ;
			}
			
			if(int.Parse(temp_line[1]) < 51)
			{
				string imsi_port = serialport1.PortName;
				//2p
				Renew_port_instance(0);
				
					if(imsi_port.Contains("COM"))
					{
						serialport2.PortName = imsi_port;
						serialport2.BaudRate = 9600;
						serialport2.ReadTimeout = 20;
						serialport2.Open ();
					}
				P2_connect = true;
				P1_connect = false;
			}
			else{
				P1_connect = true;
				P2_connect = false;
			}
		}
		else if(port1 == 1 && port2 == 1)
		{
			//Debug.Log ("Enter the port confirm!!!!!!!!!!!!!!!!!");
			port_confirm();
		}
		
    }

	void port_confirm()
	{
		
		if(serialport1 != null)
		{
			serialport1_datareceived();
			//confirm_num = check_num ;
		}
		
		if(serialport2 != null)
		{
			serialport2_datareceived();
		}
		
		if(int.Parse(temp_line[1]) < 51)
		{
			//2p
			string imsi_port = serialport1.PortName;
			string imsi_port2 = serialport2.PortName;
			
			Renew_port_instance(0);
			
				if(imsi_port.Contains("COM"))
					{
						serialport2.PortName = imsi_port;
						serialport2.BaudRate = 9600;
						serialport2.ReadTimeout = 20;
						serialport2.Open ();
						P2_connect = true;
					}
				if(imsi_port2.Contains("COM"))
					{
						serialport1.PortName = imsi_port2;
						serialport1.BaudRate = 9600;
						serialport1.ReadTimeout = 20;
						serialport1.Open ();
						P1_connect = true;
					}
		}
		else
		{
			P1_connect = true;
			P2_connect = true;
		}
	}

	
    void serial1_off()
    {
        if (serialport1.IsOpen)
        {
            serialport1.Dispose();
            serialport1.Close();
        }
    }

    void serial2_off()
    {
        if (serialport2.IsOpen)
        {
            serialport2.Dispose();
            serialport2.Close();
        }
    }


	void serialport1_datareceived()
	{
        try
        {
			imsi = serialport1.ReadLine(); 
			temp_line = imsi.Split('_');
            direction1 = temp_line[0];
			

        }
        catch (System.Exception ex)
        {

        }
	}

    void serialport2_datareceived()
    {
        try
        {
      			imsi2 = serialport2.ReadLine(); 
				temp_line2 = imsi2.Split('_');
                direction2 = temp_line2[0];
		
        }
        catch (System.Exception ex)
        {

        }
    }
}
