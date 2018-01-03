<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Admin_Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- <dl>
        <dt>
            When the film is uploaded
        </dt>
        <dd>
            Your film is uploaded successfully and we will inform you shortly once it got approved
        </dd>
    </dl>--%>

    <ul>
        <li>
             <b>When the film is uploaded :</b> Your film is uploaded successfully and we will inform you shortly once it got approved
        </li>
        <li>
           <b> When the advertisement is uploaded :</b> Your advertisement is uploaded successfully and we will inform you shortly once it got approved
        </li>
         <li>
             <b>When the trailer is uploaded :</b> Your trailer is uploaded successfully and we will inform you shortly once it got approved
        </li>
         <li>
             When play shortfilm is clicked if wallet is zero as we are redirecting to the wallet page it should be shown as "Your wallet amount is nill,Do any of the
 below processes to refill the wallet"
        </li>
         <li>
             After the playing of advertisement is finished it should be shown as "Rs.2 has been successfully added to the wallet and deducted.Enjoy the film"
        </li>
         <li>
             When play shortfilm is clicked if wallet has  >=2Rs. then it should display "Rs.2 has been deducted from your wallet.Enjoy the film"
        </li>
        </ul>
    <h4>Information to be shown</h4>
    <ul>
         <li>
             <b>Advertisements</b>
             <ul>
                 <li>
                     <b>Preferences :</b>If you select a particular film or multiple films your advertisement will only be displayed to the people who plays that or those 
                     films based on the locations you select below (1person = 1 view) <br />If you didn't select any film it would be taken as all and your advertisement will be displayed to the people in the locations you 
                     will select below (1person = 1 view)
                 </li>

                  <li>
                     <b>Importance  :</b>If you select VIEWS as option then your advertisement will be displayed in the website till the completion of the last view(based on the
                      no. of views you had kept) <br />If you select Date as an option then you are asked to select start date and the end date where your advertisement will be displayed to 
                     the people in the website only between those days and if your budget still remains unused you can get back the remaining amount(PAYMENT
                     GATEWAY charges may applicable)
                 </li>
              
                   <li>
                     <b>Upload Video :</b>It should be (<=1.05 minutes) to get approved
                 </li>
             </ul>
        </li>
           <li>
             <b>Shortfilm</b>
             <ul>
                 <li>
                     <b>Upload shortfilm :</b>It should be (<=20 minutes) to get approved 
                 </li>

                  <li>
                     <b>Create Promocode :</b><pre> unique promocode is for the people who wants to be a promoter.The duty is to bring advertisements and make them place in the website 
                   by entering the code.Then the earnings is shared for you as on the budget allocation.As of now its 10%. 
                   Click Yes to be a promoter 
                         No  to be not a promoter
                          Yes              No</pre>
                 </li>
              
             </ul>
        </li>
    </ul>
</asp:Content>

