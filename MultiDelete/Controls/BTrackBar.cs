using System;
using System.Windows.Forms;

namespace MultiDelete
{
    public class BTrackBar : TrackBar 
    {
        public int TrackBarValue { get => Value; set { 
            if(value > Maximum) {
                Value = Maximum;
                return;
            }
            if(value < Minimum) {
                Value = Minimum;
                return;
            }
            Value = value;
        } }
    }
}