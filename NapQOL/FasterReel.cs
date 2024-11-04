using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace NapQOL;

public class FasterReel : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc"; //make sure its pointing to the correct script (made that mistake) AND extension is gdc, not just gd (made that mistake too)

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        // wait for any newline after any reference to "_process"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_enter_animation"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: StringVariant {Value: "rod_retract"}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: false}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "STATES"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "DEFAULT"},
        ]);


        foreach (var token in tokens)
        {
            if (waiter.Check(token))
            {
                yield return token; //return "DEFAULT" token
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(10));

            }
            else
            {
                // return the original token (this is required in every token patch i think or else the script just wont run im guessing)
                yield return token;
            }
        }
    }
}
