import { ApiProperty } from '@nestjs/swagger';

export class VencedorDto {
  @ApiProperty()
  id: string;
  @ApiProperty()
  nome: string;
}
