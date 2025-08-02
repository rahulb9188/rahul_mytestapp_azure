import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'booleanConverter'
})
export class BooleanConverterPipe implements PipeTransform {

  transform(value: any): string {
    return value === true ? 'Yes' : value === false ? 'No' : '';
  }

}
