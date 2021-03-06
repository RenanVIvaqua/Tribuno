Create Procedure Prc_Deletar_Operacao
	@IdOperacao                      BIGINT   
    AS
	BEGIN        
        BEGIN TRAN  
            BEGIN TRY
                DELETE OperacaoParcelas WHERE IdOperacao = @IdOperacao	
	            DELETE Operacao  WHERE IdOperacao = @IdOperacao
                COMMIT TRANSACTION            
            END TRY    
            BEGIN CATCH
            IF @@TRANCOUNT > 0
            BEGIN				
                ROLLBACK TRANSACTION  
				print 'Realizado RollBack'
            END
        END CATCH
    END
