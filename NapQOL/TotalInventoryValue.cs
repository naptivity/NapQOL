using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace NapQOL;

public class TotalInventoryValue : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/HUD/playerhud.gdc"; //make sure its pointing to the correct script (made that mistake) AND extension is gdc, not just gd (made that mistake too)

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // wait for any newline after any reference to "_process"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_process"}, //this function is only ever called when the inventory has something added or removed (so when the inventory needs to be refreshed)
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);


        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                yield return token; //reason you need to return original newline at beginning  is to maintain indentation
                //yield return new Token(TokenType.Newline, 1); //note that second param in token constructor is indentation


                //var TotalInvVal = 0
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("TotalInvVal");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new IntVariant(0));
                yield return new Token(TokenType.Newline, 1);


                //for item in PlayerData.inventory:
                yield return new Token(TokenType.CfFor);
                yield return new IdentifierToken("item");
                yield return new Token(TokenType.OpIn);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("inventory");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 2);


                //var ref = item["ref"]
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("ref");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("item");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("ref"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Newline, 2);


                //var idata = PlayerData._find_item_code(ref)
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("idata");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_find_item_code");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ref");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 2);


                //var id = idata["id"]
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("idata");
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("id"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Newline, 2);


                //if not Globals.item_data.keys().has(id): continue
                yield return new Token(TokenType.CfIf);
                yield return new Token(TokenType.OpNot);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("keys");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("has");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.CfContinue);
                yield return new Token(TokenType.Newline, 2);


                //var data = Globals.item_data[id]["file"]
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("data");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("Globals");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("item_data");
                yield return new Token(TokenType.BracketOpen);
                yield return new IdentifierToken("id");
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.BracketOpen);
                yield return new ConstantToken(new StringVariant("file"));
                yield return new Token(TokenType.BracketClose);
                yield return new Token(TokenType.Newline, 2);


                //if data.can_be_sold:
                yield return new Token(TokenType.CfIf);
                yield return new IdentifierToken("data");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("can_be_sold");
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 3);


                //TotalInvVal += PlayerData._get_item_worth(ref)
                yield return new IdentifierToken("TotalInvVal");
                yield return new Token(TokenType.OpAssignAdd);
                yield return new IdentifierToken("PlayerData");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("_get_item_worth");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("ref");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline, 1);


                //get_node("MarginContainer/ScrollContainer/HBoxContainer/VBoxContainer/Label3").text = "creatures & junk ($" + str(TotalInvVal) + ")"
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("main/menu/tabs/inventory/MarginContainer/ScrollContainer/HBoxContainer/VBoxContainer/Label3"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("text");
                yield return new Token(TokenType.OpAssign);
                yield return new ConstantToken(new StringVariant("creatures & junk ($"));
                yield return new Token(TokenType.OpAdd);
                yield return new Token(TokenType.BuiltInFunc, (uint?) BuiltinFunction.TextStr); //THANK YOU NOTNITE!!!
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("TotalInvVal");
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.OpAdd);
                yield return new ConstantToken(new StringVariant(")"));
                yield return new Token(TokenType.Newline, 1);


                ////PlayerData._send_notification(TotalInvVal, 1)
                //yield return new IdentifierToken("PlayerData");
                //yield return new Token(TokenType.Period);
                //yield return new IdentifierToken("_send_notification");
                //yield return new Token(TokenType.ParenthesisOpen);
                //yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextStr);
                //yield return new Token(TokenType.ParenthesisOpen);
                //yield return new IdentifierToken("TotalInvVal");
                //yield return new Token(TokenType.ParenthesisClose);
                //yield return new Token(TokenType.Comma);
                //yield return new ConstantToken(new IntVariant(1));
                //yield return new Token(TokenType.ParenthesisClose);
                //yield return new Token(TokenType.Newline, 1);


                ////print(TotalInvVal)
                //yield return new Token(TokenType.BuiltInFunc, (uint?)BuiltinFunction.TextPrint);
                //yield return new Token(TokenType.ParenthesisOpen);
                //yield return new IdentifierToken("TotalInvVal");
                //yield return new Token(TokenType.ParenthesisClose);
                //yield return new Token(TokenType.Newline, 1);


                ////PlayerData._send_notification("ur mod works", 1)
                //yield return new IdentifierToken("PlayerData");
                //yield return new Token(TokenType.Period);
                //yield return new IdentifierToken("_send_notification");
                //yield return new Token(TokenType.ParenthesisOpen);
                //yield return new ConstantToken(new StringVariant("ur mod works"));
                //yield return new Token(TokenType.Comma);
                //yield return new ConstantToken(new IntVariant(1));
                //yield return new Token(TokenType.ParenthesisClose);
                //yield return new Token(TokenType.Newline, 1);

                yield return token; //reason you need to return original newline at end is to maintain indentation

            } else {
                // return the original token (this is required in every token patch i think or else the script just wont run im guessing)
                yield return token;
            }
        }
    }
}
