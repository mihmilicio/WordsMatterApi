import { ApiProperty } from '@nestjs/swagger';

export class VencedorDto {
  @ApiProperty()
  idSala: string;
  @ApiProperty()
  nomeParticipante: string;
}
