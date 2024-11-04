using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace NapQOL;

public class FasterFishing : IScriptMod
{
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player.gdc"; //make sure its pointing to the correct script (made that mistake) AND extension is gdc, not just gd (made that mistake too)

    // returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens)
    {
        var fishingreal_waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_enter_animation"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: StringVariant {Value: "rod_cast"}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: false}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "STATES"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "FISHING"},
        ]);
        var fishingfake_waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_enter_animation"},
            t => t.Type is TokenType.ParenthesisOpen,
            t => t is ConstantToken {Value: StringVariant {Value: "rod_cast"}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: false}},
            t => t.Type is TokenType.Comma,
            t => t is ConstantToken {Value: BoolVariant {Value: true}},
            t => t.Type is TokenType.Comma,
            t => t is IdentifierToken {Name: "STATES"},
            t => t.Type is TokenType.Period,
            t => t is IdentifierToken {Name: "FISHING_CANCEL"},
        ]);
        var retract_waiter = new MultiTokenWaiter([
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
            if (fishingreal_waiter.Check(token))
            {
                yield return token; //return "FISHING" token
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(5));

            }
            else if (fishingfake_waiter.Check(token))
            {
                yield return token; //return "FISHING_CANCEL" token
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(5));

            }
            else if (retract_waiter.Check(token))
            {
                yield return token; //return "DEFAULT" token
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new BoolVariant(true));
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new IntVariant(5));

            }
            else
            {
                // return the original token (this is required in every token patch i think or else the script just wont run im guessing)
                yield return token;
            }
        }
    }
}
