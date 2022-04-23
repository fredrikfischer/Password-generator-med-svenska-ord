using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine(GetPassword());
    }

    private static string GetPassword(){
        Random rnd = new Random();
        string password = "";
        string specialChar = GetBindingCharachter(rnd);
        int maxAntalTecken = 20;
        int maxAntalOrd = 3;
        int randomNumber = rnd.Next(100);

        for(int i = 0; password.Length<maxAntalTecken && i<maxAntalOrd; i++){
            string randomWord = "";
            bool wordbool = false;

            while(!wordbool){
                randomWord = GetRandomSwedishWord(rnd);
                //the if statement filters out certain types of words
                if(randomWord.Length<9 && !(randomWord.Contains(" ")) && !(randomWord.Contains("-"))){
                    wordbool = true;
                }
            }
            password += randomWord + specialChar;
        }

        // if statement that titlecase password if "0", Uppercase if "1" or (remains) lowercase if 2
        int rndNum = rnd.Next(3);
        if(rndNum == 0){
         password = TitleCasePassword(password);
     }else if(rndNum == 1){
         password = password.ToUpper();
     }


        //Inserts special charachters into the password
        password = MoreSpecialCharachters(rnd, password);

        // Places the special number either before or after the pass-phrases
        if(rnd.Next(2) == 0){
            return randomNumber + specialChar + password.Substring(0, password.Length-1);
        }else{
            return password + randomNumber;
        }
        
    }

    private static string GetRandomSwedishWord(Random rnd){
        string unserializedList = File.ReadAllText("svenska-ord.json");
        List <string> serializedList = JsonSerializer.Deserialize<List<string>>(unserializedList);
        int num = rnd.Next(serializedList.Count);
        return serializedList[num];
    }

    private static string GetBindingCharachter(Random rnd){
        string specialChar = " -+_,.";
        int num = rnd.Next(specialChar.Length);
        return specialChar[num].ToString();
    }

    private static string MoreSpecialCharachters(Random rnd, string password){
        bool isReplaced = false;
        string specialCharList = "11!!@@4433££$$5500µµ7766";
        string replaceCharList = "IiIiAaAaEeEeSsSsOoUuTtBb";
        int num = rnd.Next(specialCharList.Length-1);
        string replacedPassword = "";

        while(!isReplaced){
            if(password.Contains(replaceCharList[num])){
                replacedPassword = password.Replace(replaceCharList[num], specialCharList[num]);
                isReplaced = true;
            }else{
                if(num<specialCharList.Length){
                    num++;
                }else{
                    num = 0;
                }
                
            }
        }
        return replacedPassword;
    }

    private static string TitleCasePassword(string password){
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(password);
    }
}










