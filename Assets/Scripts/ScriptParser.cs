using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;


public class ScriptParser : MonoBehaviour
{
    public bool scriptChanged = false;
    public string scriptName;
    public string NPCName;
    string path { get { return Application.streamingAssetsPath + "/" + NPCName + "/" + scriptName + ".txt"; } }

    DialogueSystem dialogue;

    CommandsManager commands;
    StreamReader reader = null;

    string currentLine;

    // regex for grabbing rich text tags
    Regex richRegex = new Regex("<(.*?)>");

    // regex for grabbing expressions
    Regex emoteRegex = new Regex("\\[(.*?)\\]");

    // regex for commands
    Regex commandRegex = new Regex("\\{(.*?)\\}");




    // Start is called before the first frame update
    void Start()
    {
        commands = gameObject.GetComponent<CommandsManager>();
        dialogue = gameObject.GetComponent<DialogueSystem>();
    }

    public void initializeDialogue()
    {
        reader = new StreamReader(path);
        readNextLine();
        parseLine(currentLine);
    }
    public void changeScriptFile(string newScript)
    {
        scriptName = newScript;
        reader = new StreamReader(path);

        // readNextLine();
        // parseLine(currentLine);
        scriptChanged = false;
    }

    public void wipeDialogue()
    {
        dialogue.speechText.text = "";
    }
    // Update is called once per frame
    public void progressDialogue()
    {
        if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
        {
            // if end of file has been reached
            if (reader.Peek() == -1)
            {
                return;
            }
            // else grab next line
            readNextLine();
            parseLine(currentLine);
        }
        else
        // if the dialogue is still being written out just skip to the end of the line
        {
            dialogue.StopSpeaking();
            dialogue.speechText.text = currentLine;
        }

    }

    void parseLine(string line)
    {
        // RICH TEXT
        // Unity already supports rich text natively,
        // we just need to make sure the typewriter
        // works properly with it

        if (line == null)
        {
            return;
        }
        if (richRegex.IsMatch(line))
        {
            dialogue.SayRich(currentLine);
            return;
        }
        // handles speaker change. Also handles which character's expressions/animations are being controlled
        if (line.Contains("::"))
        {
            string newSpeaker = line.Split(new[] { "::" }, System.StringSplitOptions.None)[0];
            dialogue.currentSpeaker = newSpeaker;

            // update character sprite to current speaker sprite
            readNextLine();
            parseLine(currentLine);
            return;
        }


        // CHANGING EXPRESSIONS
        // expressions in the script will be written like this: [happy]
        // expressions must be on their own lines 
        if (emoteRegex.IsMatch(line))
        {
            // Debug.Log(line);
            char[] tagChars = { '[', ']', ' ' };
            string commandStr = line.Trim(tagChars);


            readNextLine();
            parseLine(currentLine);
            return;
        }

        // COMMANDS
        // These will be any other type of commands 
        // that aren't rich text tags or emotion controls
        if (commandRegex.IsMatch(line))
        {
            char[] tagChars = { '{', '}', ' ' };
            string command = line.Trim(tagChars);


            if (command.Contains(":"))
            {
                ArrayList commandWords = new ArrayList(command.Split(':'));
                command = commandWords.Cast<string>().ElementAt(0);
                commandWords.RemoveAt(0);
                ArrayList args = new ArrayList(commandWords[0].ToString().Split(','));

                commands.handleWithArgs(command.ToLower(), args);
            }
            else
            {
                commands.handleWithArgs(command.ToLower(), null);
            }
            if (scriptChanged)
            {
                return;
            }
            readNextLine();
            parseLine(currentLine);
            return;
        }

        // if it's not a command simply display the text
        dialogue.Say(currentLine);

    }
    private void OnApplicationQuit()
    {
        reader.Close();
    }
    void readNextLine()
    {
        currentLine = reader.ReadLine();
    }
}
