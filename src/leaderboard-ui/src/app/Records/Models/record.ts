
export class Record {
    constructor(
        public ID: number,
        public CompetitionName: string,
        public TeamName: string,
        public UserNames: string,
        public Score: number,
        public ScoreFirstSubmittedDate: Date,
        public NumSubmissions: number
        ){}

}