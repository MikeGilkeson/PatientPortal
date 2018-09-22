import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'age' })
export class agePipe implements PipeTransform {
    transform(fromDate: Date): string {
        let today = new Date();

        var age = [], fromDate = new Date(fromDate),
            year = [today.getFullYear(), fromDate.getFullYear()],
            yearDiff = year[0] - year[1],
            month = [today.getMonth(), fromDate.getMonth()],
            monthDiff = month[0] - month[1],
            day = [today.getDate(), fromDate.getDate()],
            dayDiff = day[0] - day[1];

        if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0))--yearDiff;
        if (monthDiff < 0) monthDiff += 12;
        if (dayDiff < 0) {
            fromDate.setMonth(month[1] + 1, 0);
            dayDiff = fromDate.getDate() - day[1] + day[0];
            --monthDiff;
        }
        if (yearDiff > 0) age.push(yearDiff + ' year' + (yearDiff > 1 ? 's ' : ' '));
        if (monthDiff > 0) age.push(monthDiff + ' month' + (monthDiff > 1 ? 's' : ''));
        if (dayDiff > 0) age.push(dayDiff + ' day' + (dayDiff > 1 ? 's' : ''));
        if (age.length > 1) {
            let monthAndDays = age.splice(1);
            age.push(`(${monthAndDays.join(', ')})`);
        }
        return age.join('');
    }
}