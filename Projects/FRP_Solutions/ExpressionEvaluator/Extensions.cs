using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator
{
    public static class Extensions
    {
        public static Expr ParseOne()
        {
            Expr r = new BinaryExpr(new Number(2),
               new BinaryExpr(new Number(3), new Number(4), OPERATOR.MUL),
               OPERATOR.PLUS);
            return r;
        }

        public static Expr ParseTwo()
        {
            Expr r = new UnaryExpr(new Number(10), OPERATOR.MINUS);
          
            return r;
        }

        public static List<ITEM_LIST> FlattenExprToList(this Expr e)
        {
            FlattenVisitor fl = new FlattenVisitor();
            e.accept(fl);
           return fl.FlattenedExpr();
        }
         
        public static double Evaluate( this List<ITEM_LIST> ls)
        {
            Stack<double> stk = new Stack<double>();

            foreach( ITEM_LIST s in ls )
            {
                if (s.knd == ExprKind.VALUE)
                    stk.Push(s.Value);
                else
                {
                    switch(s.op) {
                        case OPERATOR.PLUS:
                            stk.Push(stk.Pop() + stk.Pop());
                            break;
                        case OPERATOR.MINUS:
                            stk.Push(stk.Pop() - stk.Pop());
                            break;
                        case OPERATOR.DIV:
                            double n = stk.Pop();
                            stk.Push(stk.Pop() / n);
                            break;
                        case OPERATOR.MUL:
                            stk.Push(stk.Pop() * stk.Pop());

                            break;

                    }


                }


            }
            return stk.Pop();
        }
    }
}
