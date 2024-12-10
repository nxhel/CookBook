namespace cookBook{
    public class Instruction{

        public int InstructionId { get; set; }

        private string _instructions { get; set;}
        public string Instructions{
            get =>_instructions;
            set{
                if(string.IsNullOrEmpty(value)){
                    throw new ArgumentException("Instruction cannot be empty");
                }
                _instructions = value;
            }
        }
        public Instruction(string step)
        {
            _instructions=step;
        }
        public Instruction(){}
    }
}

