import {Visit} from './visit.model';

export class Patient  {
    constructor(
        public id: number,
        public firstName?: string,
        public lastName?: string,
        public dateOfBirth?: Date,
        public gender?: string,
        public ethnicityCode?: string,
        public lastVisit?: Date,
        public nextVisit?: Date,
        public alerts?: [string, string],
        public visits?: [Visit]
    ) { }
}