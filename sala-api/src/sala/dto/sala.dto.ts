import { ApiProperty, ApiPropertyOptional } from '@nestjs/swagger';

export class SalaDto {
  @ApiProperty()
  nome: string;
  @ApiProperty()
  tema: string;
  @ApiPropertyOptional()
  maxEnvios?: number;
}
