using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Windows;
using System.Xml.Linq;

namespace SpeachSynthesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synthesis = new SpeechSynthesizer();

            ReadOnlyCollection<InstalledVoice> voices = synthesis.GetInstalledVoices();
            //synthesis.SetOutputToDefaultAudioDevice();
            //  AudioBitsPerSample audioBitsPerSample = new AudioBitsPerSample();

            //SpeechAudioFormatInfo formatInfo = new SpeechAudioFormatInfo((int)EncodingFormat.ALaw, audioBitsPerSample, AudioChannel.Stereo);
            //synthesis.Rate = 0; // speed at which the person speaks -10 to 10
            // synthesis.Volume = 50;
            synthesis.SelectVoice("Microsoft Zira Desktop");
           
            synthesis.SetOutputToWaveFile(@"C:\Users\hkhan\Desktop\Sample.wav", new SpeechAudioFormatInfo(16000, AudioBitsPerSample.Eight, AudioChannel.Mono));
            synthesis.SpeakCompleted += Synthesis_SpeakCompleted;

            string filepath = Path.Combine(Environment.CurrentDirectory, "TalkFile.ssml");
        
            synthesis.SpeakAsync(Speakthis());

            //synthesis.Dispose();
        }

        private void Synthesis_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            // Create a SoundPlayer instance to play the output audio file.
            System.Media.SoundPlayer m_SoundPlayer =
              new System.Media.SoundPlayer(@"C:\Users\hkhan\Desktop\Sample.wav");

            //  Play the output file.
            m_SoundPlayer.Play();
        }
        public PromptBuilder Speakthis()
        {
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText(@"
1.
Pre-Conditions: (For QA, off/on premise testing)
- Execute TC # 27443, If you don't have cache connection setup in Redis Desktop Manager for Environment - AccountID
- Execute TC # 27852, If you don't have latest IDSCoreExternalApi postman collection and IDSPostmanEnvironments file is postman.     

 
- User has Redis Desktop Manager installed on QA VM. 
- User has Postman desktop app Installed on QA VM.   

Environment = mbqa - off premise
AccountID = 501
2.
Login to G2 with valid UserID and Password. 

EXPECTED RESULT
User is logged into G2 Successfully. 

3.
Go to 'Admin' - Setup - Update Codes and Tables - Service - Internal Bill Codes

Search for an existing Bill Code which is setup for Capitalize to Inventory OR
follow the steps below to create new.  
 
- Click on blank Paper icon on top left to create a new Bill Code.
- Enter Bill Code (e.g. 'PDI') - Enter Description.
- Enter I in GL Account (I to capatilize) field. 
 (The filed below is populated with text, 'Capitalize to inventory')
- Click on Save icon 

Note: make note of the Bill Code (e.g. PDI)

EXPECTED RESULT
A pop-up is displayed with an information message.
Internal bill code <Bill Code> has been updated.  

4.
Note: Note down the original value of config # 26 before updating it below, revert the changes once testing is done.

Go to 'Admin' - Setup - Update Module Configurations - Service tab
 
 - Config # 26 (W/O Bill Code for PDI Work), 
Double click on value of the config,
From Entries Available for WO.BILL.CODE window, select Bill Code created above. (e.g. PDI) 

Make note of Value of Config # 26. 

Click OK 

EXPECTED RESULT
The selected Bill Code is displayed as value of Config # 26. (e.g. PDI)

5.
Go to 'Service' - Work Order - Update Work Orders
- Click on blank Paper icon on top left to create a new Work Order.
- Search or enter a Cust#
- Search or enter a Stock#
- Enter details in all other required fields (like Sales ID, Expected/Promised Date)

Switch the work order in Actuals mode  
 
- 'Add Job'
- Enter Bill Type: I  -> Enter Bill To (Here, enter Bill Code(e.g. PDI) which was set as Value of Config # 26) -> Enter 'Job Description'

- Add Parts
- Add Labor


Make note of User's Logged In Location, Work Order #, WO Date, Stock#, Bill To code and Total of Actual amount of WO job. 

Save the WO. 

EXPECTED RESULT
An pop up is displayed with an information message and OK button.
Work order # <WO#> updated.  

Work Order details are displayed ON SCREEN.
 

6.
Click on 'Functions > Finalize Work Order'
- Enter Completed Date,
- Enter Pickup Date

Click 'OK' 



Note: Make note of Completed Date and Keep the Work Order Open in G2. 

EXPECTED RESULT
A pop up with an information message is displayed.
Work order # <WO#> updated. 

Click 'OK'

Another pop up with a message is displayed.
Work order was completed  on <Completed Date>.  

7.
- Launch 'Astra' -  INPUT NUMBER OF MENU: 1 -> 2 -> 4
- On 'DISPLAY INVENTORY' screen, enter 'Stock#' (from Step # 5)
(Stock # details are displayed on screen).
 
- Enter 'WO' in FIELD# at bottom left of the screen - PRESS 'Enter'
 
 

EXPECTED RESULT
- Stock # details are displayed in first half of the page.
 -  Under WORK ORDERS section,
search for W/O # created at step # 5 and verify the following details for both jobs.


W/O # = W/O # created at step # 5 
Bill ID =  Bill Code set as Value of Config # 26 
PDI = 0.00
Est/Unp = 0.00
Est/Posted = 0.00
Unbilled = 0.00
Finalized = Total of Actual amount.    

8.
KEEP THE WORK ORDER DETAILS OPEN IN ASTRA

9.
On the following step # 10.2 after selecting the API_Request,
Click on eye icon in the top right corner, Search for {{TestingSingleStockNo}} under first column VARIABLE, hovor on its value under third Column CURRENT VALUE.
Click on Edit icon and chage its value to Stock # created in G2. 

API_Request = IDSCoreExternalApi - Select folder Units/Location/Id - Select Units GET Testing - Select Api request v1/Units GET - 01
10.
Postman - Send API request
10.1
From your QA VM, Run Postman app. 

EXPECTED RESULT
 Postman app is opened successfully.

10.2
Now, select an Api request.
 
From Environments drop down - select Environment, click on the eye icon next to it and ensure that <AccountID> matches the AccountID 

From Collections, Click on IDSCoreExternalApi - select folder, API_Request

Environment = mbqa - off premise
AccountID = 501
API_Request = IDSCoreExternalApi - Select folder Units/Location/Id - Select Units GET Testing - Select Api request v1/Units GET - 01
EXPECTED RESULT
On right hand side, In first half of the page,
 The selected request is displayed with Request URL, Headers and Body as defined in the request.

Tests tab displays the Test Script. (If exists)

10.3
Now, click on Send button to send the request for Dealer:AccountID. 

AccountID = 501
EXPECTED RESULT
On right hand side, In the second half of the page
 - Response is received successfully with Status
- Response body is displayed in Body tab. 
- Response Headers are displayed in Headers tab.
- Test Results are displayed in Test Results tab.
(If any Test Script exists for selected request)  

Status = 200 OK
11.
Respone - Body tab
- Validate that you get Work Order details against the stock # you entered in {{TestingSingleStockNo}}.

Match the work order details with WO in Astra.
  

EXPECTED RESULT
Units Api returns Work Order details against entered {{TestingSingleStockNo}}.
 
Get the values highlighted below in BOLD from Astra and in italic from G2, match with WO details returned by API.  

{
  WorkOrders: [
    {
      WorkOrderNo: string, = W/O #
      WorkOrderLocation: string, = User's Logged In Location in G2
      WorkOrderBillId: string, = Bill ID
      WorkOrderPDI: string, = PDI
      WorkOrderEstimatedUnposted: string, = Est/Unp
      WorkOrderEstimatedPosted: string, = Est/Posted
      WorkOrderUnbilled: string, = Unbilled
      WorkOrderFinalized: string, = Finalized
      WorkOrderDateOpen: WO Date (from G2 work order window),
      WorkOrderDateClosed: Completed Date (entered while finalizing the work order)
    }
  ]
} 

12.
Response Body - 'Test Results' tab

When response is returned then an automated Test Script is executed and validates the following.
- Response Status Code --- which must be '200 OK' 
- The data type of fields returned in 'Work Orders' section. 
- All required fields are returned?



All above Tests must be displayed with Status PASS.  

EXPECTED RESULT
Test Results are displayed in the following sequence.
 
- PASS Status code is 200
- PASS Work Orders Data is Valid.
- PASS No Errors, All required fields are present with valid data! 

If any of the above Test is Failed then an error message is displayed with Status FAIL  

13.
Before executing the next steps, ensure that you have cache connection setup in Redis for your Environment - AccountID and you have connection details as noted down at the end of TC # 27443. 

Environment = mbqa - off premise
AccountID = 501
14.
Redis Desktop Manager - validate records in an unpaged manner .
14.1
QA VM > Launch Redis Desktop Manager.  

EXPECTED RESULT
 Redis Desktop Manager is launched successfully. 

14.2
select Connection Name - click on refresh icon,
select db node - Expand ACCOUNTID - Expand AccountID - Expand LOCATION - Expand <Loaction Name> - Expand Module

Validate that the 'keys/records names are displayed in following format.
ACCOUNTID: <AccountID>: LOCATION: <Location Name>: <Module>: <Record Name>  

AccountID = 501
Module = INVENTORY
EXPECTED RESULT
- All records are cached in the right db node. 
- Keys/Records are displayed in the following format and when any of the key/record is selected then It's details are displayed on right hand side of the window.
 
 ACCOUNTID: <AccountID>: LOCATION: <Location Name>: <Module>: <Record Name>  

AccountID = 501
Module = INVENTORY
                                ");
            return builder;
        }
       
    }
}
