-- Trigger: update_inventory_auditcolumns

-- DROP TRIGGER update_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER update_inventory_auditcolumns
    BEFORE UPDATE 
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE public.update_audit_columns();