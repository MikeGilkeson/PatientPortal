import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'gender' })
export class genderPipe implements PipeTransform {
    transform(gender: string): string {
        if (gender.toLowerCase() === 'm') {
            return "Male";
        }
        if (gender.toLowerCase() === 'f') {
            return "Female";
        }
        return gender;
    }
}